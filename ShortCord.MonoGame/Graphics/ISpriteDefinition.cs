using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShortCord.MonoGame.Graphics {
    public interface ISpriteDefinition : IDisposable {

        Vector2 Position { get; set; }
        Color Mask { get; set; }
        Vector2 Origin { get; }
        Texture2D Texture { get; }
        float Rotation { get; set; }

        void Draw(SpriteBatch spriteBatch, bool debugDraw = false);
        void GenericDraw(SpriteBatch spriteBatch);
        void DebugDraw(SpriteBatch spriteBatch);

        Texture2D ToTexture2D();

    }
}
