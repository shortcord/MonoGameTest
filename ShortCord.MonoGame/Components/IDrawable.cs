using Microsoft.Xna.Framework.Graphics;

namespace ShortCord.MonoGame.Components {
    public interface IDrawable {
        bool GameDrawEnabled { get; }
        bool UiDrawEnabled { get; }

        void LoadContent();
        void UnloadContent();
        void GameDraw(SpriteBatch spriteBatch);
        void UiDraw(UiSpriteBatch spriteBatch);
    }
}
