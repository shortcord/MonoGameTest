using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShortCord.MonoGame.Graphics {
    /// <summary>
    /// A Definition of a 2D Sprite
    /// </summary>
    public class SpriteDefinition {
        /// <summary>
        /// Current <see cref="Texture2D"/>
        /// </summary>
        public Texture2D Texture { get; set; }
        /// <summary>
        /// Current Position in <see cref="Vector2"/>
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// Current Origin in <see cref="Vector2"/>
        /// </summary>
        public Vector2 Origin { get { return new Vector2(Texture.Width, Texture.Height) / 2f; } }
        /// <summary>
        /// Current <see cref="Color"/> mask
        /// </summary>
        public Color Mask { get; set; }
        /// <summary>
        /// Current Rotation in <see cref="float"/>
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// Create a new Instantce of <see cref="SpriteDefinition"/>
        /// </summary>
        /// <param name="texture"><see cref="Texture"/> of Sprite</param>
        /// <param name="position"><see cref="Position"/> of Sprite</param>
        /// <param name="rotation"><see cref="Rotation"/> of Sprite</param>
        /// <param name="mask"><see cref="Mask"/> of Sprite</param>
        public SpriteDefinition(Texture2D texture, Vector2? position = null, float rotation = 0f, Color? mask = null) {
            Texture = texture;
            Position = position ?? Vector2.Zero;
            Rotation = rotation;
            Mask = mask ?? Color.White;
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
                color: Mask
                );
        }

        /// <summary>
        /// Debug draw; Shows Bounding box and Origin of Sprite<para/>
        /// This does degrade performance
        /// </summary>
        /// <param name="spriteBatch">The <see cref="SpriteBatch"/> to draw with</param>
        public void DebugDraw(SpriteBatch spriteBatch) {
            //bounds
            var tmpRect = Texture.Bounds;
            tmpRect.Location = Position.ToPoint();
            spriteBatch.Draw(Utilities.CreateBorderedTexture(),
                destinationRectangle: tmpRect,
                origin: tmpRect.Size.ToVector2() / 2f,
                color: Color.White);

            //origin
            //upper lefthand corner
            var originText = Utilities.CreateBasicTexture(10, 10, Color.Green);
            spriteBatch.Draw(originText,
                position: Position - originText.Bounds.Size.ToVector2() / 2f,
                color: Color.White);
        }

        /// <summary>
        /// Converts <see cref="SpriteDefinition"/> to <see cref="Texture2D"/>
        /// </summary>
        /// <returns><see cref="Texture2D"/> of <see cref="SpriteDefinition"/></returns>
        public Texture2D ToTexture2D() {
            return Texture;
        }
    }
}
