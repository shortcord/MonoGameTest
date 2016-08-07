using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShortCord.MonoGame.Graphics {
    public abstract class SpriteDefinitionBase : ISpriteDefinition {
        Texture2D _debugBounds, _debugOrigin; //debug stuff to keep the GC happy

        Texture2D _texture;
        public virtual Texture2D Texture {
            get { return _texture; }
            set {
                _texture = value.Copy();
                //create when setting the texture due to not wanting a :base() call
                _debugBounds = Utilities.CreateBorderedTexture(_texture.Bounds.Size);
            }
        }
        public virtual Color Mask { get; set; }
        public virtual float Rotation { get; set; }
        public virtual Vector2 Position { get; set; }
        public virtual Vector2 Origin { get { return Texture.Bounds.Size.ToVector2() / 2f; } }


        public SpriteDefinitionBase() {
            _debugOrigin = Utilities.CreateBasicTexture(new Point(10), Color.Green);
        }

        public void Draw(SpriteBatch spriteBatch, bool debugDraw = false) {
            GenericDraw(spriteBatch);

            if (debugDraw) {
                DebugDraw(spriteBatch);
            }
        }

        /// <summary>
        /// Basic Draw method
        /// </summary>
        /// <param name="spriteBatch">The <see cref="SpriteBatch"/> to draw with</param>
        public void GenericDraw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Texture,
                position: Position,
                rotation: Rotation,
                origin: Origin,
                color: Mask);
        }

        /// <summary>
        /// Debug draw; Shows Bounding box and Origin of Sprite<para/>
        /// This does degrade performance
        /// </summary>
        /// <param name="spriteBatch">The <see cref="SpriteBatch"/> to draw with</param>
        public void DebugDraw(SpriteBatch spriteBatch) {
            //bounds
            spriteBatch.Draw(_debugBounds,
                position: Position,
                rotation: Rotation,
                origin: Origin,
                color: Color.White);

            //origin
            //upper lefthand corner
            spriteBatch.Draw(_debugOrigin,
                position: Position - _debugOrigin.Bounds.Size.ToVector2() / 2f,
                color: Color.White);
        }

        /// <summary>
        /// Converts <see cref="SpriteDefinition"/> to <see cref="Texture2D"/>
        /// </summary>
        /// <returns><see cref="Texture2D"/> of <see cref="SpriteDefinition"/></returns>
        public Texture2D ToTexture2D() {
            return Texture;
        }

        public void Dispose() {
            Logger.WriteLine($"[Cleanup] {this}({this.GetHashCode()}) disposed | {Position}");
            Texture.Dispose();
        }
    }
}
