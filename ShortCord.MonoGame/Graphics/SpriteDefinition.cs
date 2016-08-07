using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShortCord.MonoGame.Graphics {
    /// <summary>
    /// A Definition of a 2D Sprite
    /// </summary>
    public class SpriteDefinition: SpriteDefinitionBase {
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
    }
}
