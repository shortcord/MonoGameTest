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

namespace ShortCord.MonoGame {
    public class GameWrapper : Game {
        protected ComponentCollection GameComponents;

        SpriteBatch spriteBatch;
        UiSpriteBatch uiSpriteBatch;

        public float FixedUpdateLoopTime { get; protected set; } = 1 / 60f;

        public LevelObject CurrentLevel {
            get { return _currentLevel; }
            set {
                _currentLevel?.Dispose();
                _currentLevel = value;
            }
        }
        volatile LevelObject _currentLevel;

        public GameWrapper(string WindowTitle, Point? WindowSize = null) : base() {
            ServiceManager.Game = this;
            base.Window.Title = WindowTitle;

            GameComponents = new ComponentCollection();

            if (WindowSize == null) {
                WindowSize = new Point(800, 600);
            }

            ServiceManager.AddService(new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = WindowSize.Value.X,
                PreferredBackBufferHeight = WindowSize.Value.Y
            });
            ServiceManager.AddService(Services);
            ServiceManager.AddService(new ContentManager(Services, "Content"));
        }

        protected override void Initialize() {
            CurrentLevel?.Start();
            base.Initialize();
        }

        protected override void LoadContent() {
            ServiceManager.AddService(GraphicsDevice);
            ServiceManager.AddService(spriteBatch = new SpriteBatch(GraphicsDevice));
            ServiceManager.AddService(uiSpriteBatch = new UiSpriteBatch(GraphicsDevice));

            CurrentLevel?.LoadContent();
        }

        float accumlator, delta;

        protected override void Update(GameTime gameTime) {
            //implementation credit @Quincy9000
            //https://github.com/Quincy9000/QuincyGameEnginePractice/blob/master/Code/Engine/HelperClasses/Scene.cs#L98
            accumlator += delta = MathHelper.Clamp(gameTime.ElapsedGameTime.TotalSeconds.ToFloat(), 0f, .25f); //add to accumlator before calling update
            
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
                Logger.WriteLine($"FixedUpdate Invoke Delta is: {accumlator} | {FixedUpdateLoopTime}");
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
            spriteBatch.Begin();

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
            uiSpriteBatch.Begin();

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
    }
}
