using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ShortCord.MonoGame.Components;

namespace ShortCord.MonoGame.Camera {
    public class CameraObject : GameObject, ICamera {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Size { get { return new Vector2(_viewport.Width, _viewport.Height); } }
        public Vector2 Origin { get { return Size / 2f; } }
        public float Rotation { get; set; } = 0f;
        public float Zoom { get; set; } = 1f;

        protected GraphicsDeviceManager _graphicsDeviceManager;
        protected Viewport _viewport;

        public Matrix TransformationMatrix {
            get {
                return
                    Matrix.CreateTranslation(new Vector3(-Position, 0f))
                    * Matrix.CreateTranslation(new Vector3(-Origin, 0f))
                    * Matrix.CreateRotationZ(Rotation)
                    * Matrix.CreateScale(Zoom)
                    * Matrix.CreateTranslation(new Vector3(_viewport.Bounds.Width * 0.5f, _viewport.Bounds.Height * 0.5f, 0f));
            }
        }

        public Rectangle VisibleArea {
            get {
                Vector2 viewportVector = new Vector2(_viewport.Bounds.Width, _viewport.Bounds.Height);
                Matrix invertedTransformation = Matrix.Invert(TransformationMatrix);
                Vector2 topLeft = Vector2.Transform(Vector2.Zero, invertedTransformation);
                Vector2 bottomRight = Vector2.Transform(viewportVector, invertedTransformation);

                return new Rectanglef(topLeft, (bottomRight - topLeft)).ToRectangle();
            }
        }

        public override void Start() {
            _graphicsDeviceManager = ServiceManager.GetService<GraphicsDeviceManager>();
            _viewport = _graphicsDeviceManager.GraphicsDevice.Viewport;
        }

        public override void LoadContent() {
            IsReady = true;
        }

        public Vector2 ScreenToWorld(Vector2 mouseLocation) {
            return Vector2.Transform(mouseLocation, Matrix.Invert(TransformationMatrix));
        }

        public Vector2 WorldToScreen(Vector2 mouseLocation) {
            return Vector2.Transform(mouseLocation, TransformationMatrix);
        }

        public override void Dispose() {
            _graphicsDeviceManager = null;
        }
    }
}
