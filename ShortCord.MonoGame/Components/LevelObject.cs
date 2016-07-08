using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ShortCord.MonoGame.Components {
    public abstract class LevelObject : GameObject, IGameLevel, IDisposable {

        public GameLevelDetails Details { get; protected set; }

        protected ContentManager Content { get; set; }

        protected LevelObject() {
            Content = new ContentManager(ServiceManager.GetService<GameServiceContainer>(), "Content");
        }

        public virtual void UnloadContent() {
            Content.Dispose();
        }
        public virtual void Dispose() {
            UnloadContent();
        }
    }
}
