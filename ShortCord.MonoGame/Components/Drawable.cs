using Microsoft.Xna.Framework.Graphics;

namespace ShortCord.MonoGame.Components {
    public abstract class Drawable : IDrawable, IComponent {
        public bool GameDrawEnabled { get; protected set; } = false;
        public bool UiDrawEnabled { get; protected set; } = false;

        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }
        public virtual void GameDraw(SpriteBatch spriteBatch, bool debugDraw) { }
        public virtual void UiDraw(UiSpriteBatch spriteBatch, bool debugDraw) { }
    }
}
