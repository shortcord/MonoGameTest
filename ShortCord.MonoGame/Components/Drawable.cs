using Microsoft.Xna.Framework.Graphics;

namespace ShortCord.MonoGame.Components {
    public abstract class Drawable : IDrawable {
        public bool GameDrawEnabled { get; protected set; }
        public bool UiDrawEnabled { get; protected set; }

        protected Drawable() {
            GameDrawEnabled = true;
            UiDrawEnabled = true;
        }

        public virtual void GameDraw(SpriteBatch spriteBatch) { }
        public virtual void UiDraw(SpriteBatch spriteBatch) { }
    }
}
