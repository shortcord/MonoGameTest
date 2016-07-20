using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ShortCord.MonoGame.Collections;
using ShortCord.MonoGame.Components;
using ShortCord.MonoGame.Extensions;
using ShortCord.MonoGame.Camera;

namespace ShortCord.MonoGame {
    public class GameWrapper : Game {
        protected ComponentCollection GameComponents;

        SpriteBatch spriteBatch;
        UiSpriteBatch uiSpriteBatch;

        readonly LevelCamera _camera;

        #region Events
        /// <summary>
        /// Event called before <see cref="CurrentLevel.GameDraw(SpriteBatch)"/>
        /// </summary>
        public event EventHandler<SpriteBatch> ExtraBeforeGameDraw;

        /// <summary>
        /// Event called before <see cref="CurrentLevel.UiDraw(UiSpriteBatch)"/>
        /// </summary>
        public event EventHandler<UiSpriteBatch> ExtraBeforeUiDraw;

        /// <summary>
        /// Event called before <see cref="CurrentLevel.Update(float)"/> 
        /// </summary>
        public event EventHandler<float> ExtraBeforeUpdate;

        /// <summary>
        /// Event called before <see cref="CurrentLevel.FixedUpdate(float?)"/>
        /// </summary>
        public event EventHandler<float?> ExtraBeforeFixedUpdate;

        /// <summary>
        /// Event called when <see cref="CurrentLevel"/> changes
        /// </summary>
        public event EventHandler<GameLevelDetails> CurrentLevelChanged;
        #endregion

        public float FixedUpdateLoopTime { get; protected set; } = 1 / 60f;

        public Matrix GameDrawTransformationMatrix { get; set; } = Matrix.Identity;

        public Dictionary<string, LevelObject> Levels { get; protected set; }

        public LevelObject CurrentLevel {
            get { return _currentLevel; }
            set {

                CurrentLevelChanged?.Invoke(this, value.Details);

                _currentLevel?.Dispose();
                _currentLevel = value;
            }
        }
        LevelObject _currentLevel;

        public GameWrapper(string WindowTitle, Point? WindowSize = null) : base() {
            ServiceManager.Game = this;
            base.Window.Title = WindowTitle;

            _camera = new LevelCamera(true);


            GameComponents = new ComponentCollection();
            Levels = new Dictionary<string, LevelObject>();

            if (WindowSize == null) {
                WindowSize = new Point(800, 600);
            }

            GameComponents.Add(_camera);

            ServiceManager.AddService(_camera); //maybe?

            ServiceManager.AddService(new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = WindowSize.Value.X,
                PreferredBackBufferHeight = WindowSize.Value.Y
            });
            ServiceManager.AddService(Services);
            ServiceManager.AddService(new ContentManager(Services, "Content"));
        }

        protected override void Initialize() {
            foreach (var item in GameComponents) {
                var tmpItem = item as GameObject;
                if (tmpItem != null) {
                    tmpItem.Start();
                }
            }

            CurrentLevel?.Start();
            base.Initialize();
        }

        protected override void LoadContent() {
            ServiceManager.AddService(GraphicsDevice);
            ServiceManager.AddService(spriteBatch = new SpriteBatch(GraphicsDevice));
            ServiceManager.AddService(uiSpriteBatch = new UiSpriteBatch(GraphicsDevice));

            foreach (var item in GameComponents) {
                var tmpItem = item as GameObject;
                if (tmpItem != null) {
                    tmpItem.LoadContent();
                }
            }

            CurrentLevel?.LoadContent();
        }

        float accumlator, delta;

        protected override void Update(GameTime gameTime) {
            //implementation credit @Quincy9000
            //https://github.com/Quincy9000/QuincyGameEnginePractice/blob/master/Code/Engine/HelperClasses/Scene.cs#L98
            accumlator += delta = MathHelper.Clamp(gameTime.ElapsedGameTime.TotalSeconds.ToFloat(), 0f, .25f); //add to accumlator before calling update

            ExtraBeforeUpdate?.Invoke(this, delta);

            //call level update first
            if (CurrentLevel != null && CurrentLevel.UpdateEnabled) {
                CurrentLevel?.Update(delta);
            }

            //call unreliable update
            foreach (var item in GameComponents) {
                var tmpItem = item as IUpdatable;
                if (tmpItem != null) {
                    if (tmpItem.UpdateEnabled) {
                        tmpItem.Update(delta);
                    }
                }
            }

            //call fixed update last
            while (accumlator >= FixedUpdateLoopTime) {
                //Logger.WriteLine($"FixedUpdate Invoke Delta is: {accumlator} | {FixedUpdateLoopTime}");
                ExtraBeforeFixedUpdate?.Invoke(this, FixedUpdateLoopTime);
                FixedUpdate(FixedUpdateLoopTime);
                accumlator -= FixedUpdateLoopTime;
            }
        }

        protected virtual void FixedUpdate(float? delta) {
            //call the level update first
            if (CurrentLevel != null && CurrentLevel.FixedUpdateEnabled) {
                CurrentLevel?.FixedUpdate(delta);
            }
            
            foreach (var item in GameComponents) {
                var tmpItem = item as IUpdatable;
                if (tmpItem != null) {
                    if (tmpItem.FixedUpdateEnabled) {
                        tmpItem.FixedUpdate(delta);
                    }
                }
            }
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Pink);
            spriteBatch.Begin(transformMatrix: _camera.TransformationMatrix);

            ExtraBeforeGameDraw?.Invoke(this, spriteBatch);

            if (CurrentLevel != null && CurrentLevel.GameDrawEnabled) {
                CurrentLevel?.GameDraw(spriteBatch);
            }

            foreach (var item in GameComponents) {
                var tmpItem = item as Components.IDrawable;
                if (tmpItem != null) {
                    if (tmpItem.GameDrawEnabled) {
                        tmpItem.GameDraw(spriteBatch);
                    }
                }
            }
            spriteBatch.End();

            //Draw Ui
            this.UiDraw();
        }

        protected void UiDraw() {
            uiSpriteBatch.Begin(SpriteSortMode.FrontToBack);

            ExtraBeforeUiDraw?.Invoke(this, uiSpriteBatch);

            if (CurrentLevel != null && CurrentLevel.UiDrawEnabled) {
                CurrentLevel?.UiDraw(uiSpriteBatch);
            }

            foreach (var item in GameComponents) {
                var tmpItem = item as Components.IDrawable;
                if (tmpItem != null) {
                    if (tmpItem.UiDrawEnabled) {
                        tmpItem.UiDraw(uiSpriteBatch);
                    }
                }
            }
            uiSpriteBatch.End();
        }

        protected override void UnloadContent() {
            CurrentLevel?.UnloadContent();
            base.UnloadContent();
        }

        protected override void Dispose(bool disposing) {
            CurrentLevel?.Dispose();
            base.Dispose(disposing);
        }
    }
}
