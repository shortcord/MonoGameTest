using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShortCord.MonoGame {
    /// <summary>
    /// Automated <see cref="RenderTarget2D"/> State Machine
    /// </summary>
    public class RenderTargetAutomation : IDisposable {

        RenderTarget2D _target;
        SpriteBatch _batch;
        Point _size;
        GraphicsDevice _device;
        bool _drawBegin;

        /// <summary>
        /// Creates a new Instance of <see cref="RenderTargetAutomation"/>
        /// </summary>
        /// <param name="Size">Size of wanted Texture2D</param>
        public RenderTargetAutomation(Point? Size = null) {
            _size = Size ?? new Point(5);
        }

        /// <summary>
        /// Redirects <see cref="GraphicsDevice"/>'s <see cref="RenderTarget2D"/> and allows for drawing
        /// </summary>
        public void Begin() {
            if (_drawBegin) {
                throw new InvalidOperationException("Begin() called more than once; Must call End() before calling Begin() again.");
            }

            _drawBegin = true;
            _device = Utilities.graphicsDevice;
            _target = new RenderTarget2D(_device, _size.X, _size.Y);
            _batch = new SpriteBatch(_device);
            _device.SetRenderTarget(_target);
            _device.Clear(Color.Transparent);
            _batch.Begin();
        }

        /// <summary>
        /// Draw to the <see cref="RenderTarget2D"/>, paramerts are the same as <see cref="SpriteBatch.Draw(Texture2D, Vector2, Color)"/>
        /// </summary>
        public void Draw(Texture2D texture, Vector2 position, Color color) => _batch.Draw(texture, position, color);

        /// <summary>
        /// Resets <see cref="GraphicsDevice"/>'s <see cref="RenderTarget2D"/> and returns <see cref="Texture2D"/> of what was drawn
        /// </summary>
        /// <returns><see cref="Texture2D"/> of what was Drawn</returns>
        public Texture2D End() {
            if (!_drawBegin) {
                throw new InvalidOperationException("Must call Begin() before End().");
            }

            _drawBegin = false;
            _batch.End();
            _device.SetRenderTarget(null);
            return _target;
        }

        public void Dispose() {
            _batch.Dispose();
            _batch = null;
            _device = null;
            _target = null;
        }


    }
}
