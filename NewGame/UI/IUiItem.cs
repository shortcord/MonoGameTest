using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NewGame.UI {
    interface IUiItem {
        Rectangle RenderRectangle { get; }
        void Draw(SpriteBatch spriteBatch);
        void Update(float deltaTime);
    }
}
