using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShortCord.MonoGame.Graphics;

namespace ShortCord.MonoGame.Camera {
    public class LevelCamera : CameraObject {

        SpriteDefinition _crosshair;
        SpriteFont _font;
        Input _input;

        public LevelCamera() : base() {
            UiDrawEnabled = true;
            UpdateEnabled = true;
        }

        public override void Start() {
            base.Start();
            _input = ServiceManager.GetService<Input>();
            ServiceManager.Game.Window.ClientSizeChanged += Window_ClientSizeChanged;
        }


        void Window_ClientSizeChanged(object sender, EventArgs e) {
            _viewport = _graphicsDeviceManager.GraphicsDevice.Viewport;
        }

        public override void LoadContent() {
            _font = ServiceManager.GetService<ContentManager>().Load<SpriteFont>("Font");

            _crosshair = Utilities.CreateCrossTexture();

            base.LoadContent();
        }

        public override void Update(float delta) {
            //Position = _input.CurrentMouseState.PositionV - Origin;
            mousePos = _input.CurrentMouseState.PositionV;
            StWmousePos = ScreenToWorld(mousePos);
            WtSmousePos = WorldToScreen(mousePos);

            _crosshair.Position = mousePos;
        }

        public Vector2 mousePos, StWmousePos, WtSmousePos;

        public override void UiDraw(UiSpriteBatch spriteBatch, bool debugDraw) {
            if (IsReady) {

                spriteBatch.DrawString(spriteFont: _font,
                                       text: $"Camera {Position} {Origin}\n{Rotation} {Zoom}\n{Size}\n{mousePos} {StWmousePos} {WtSmousePos}",
                                       position: Vector2.Zero,
                                       color: Color.Black);

                if (debugDraw) {
                    spriteBatch.Draw(Utilities.CreateBasicTexture(), VisibleArea.Center.ToVector2(), new Color(Color.Black, 0.3f));
                    _crosshair.DebugDraw(spriteBatch);
                }

                _crosshair.GenericDraw(spriteBatch);
            }
        }

        public override void Dispose() {
            ServiceManager.Game.Window.ClientSizeChanged -= Window_ClientSizeChanged;
            base.Dispose();
        }
    }
}
