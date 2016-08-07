using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ShortCord.MonoGame.Collections;

namespace ShortCord.MonoGame.Components {
    public abstract class LevelObject : GameObject, IGameLevel {

        public GameLevelDetails Details { get; protected set; }
        public bool Debug { get; protected set; }

        protected ContentManager Content { get; set; }
        protected GameObjectCollection Objects { get; set; }
        protected LevelObject() {
            Content = new ContentManager(ServiceManager.GetService<GameServiceContainer>(), "Content");
            Objects = new GameObjectCollection();
        }

        public override void Start() {
            foreach (var item in Objects) {
                item.Start();
            }
        }

        public override void LoadContent() {
            foreach (var item in Objects) {
                item.LoadContent();
            }
        }

        public override void Update(float delta) {
            foreach (var item in Objects) {
                if (item.UpdateEnabled) {
                    item.Update(delta);
                }
            }
        }

        public override void FixedUpdate(float? delta) {
            foreach (var item in Objects) {
                if (item.FixedUpdateEnabled) {
                    item.FixedUpdate(delta);
                }
            }
        }

        public override void GameDraw(SpriteBatch spriteBatch, bool debugDraw) {
            foreach (var item in Objects) {
               if (item.GameDrawEnabled) {
                    item.GameDraw(spriteBatch, Debug);
                }
            }
        }

        public override void UiDraw(UiSpriteBatch spriteBatch, bool debugDraw) {
            foreach (var item in Objects) {
                if (item.UiDrawEnabled) {
                    item.UiDraw(spriteBatch, Debug);
                }
            }
        }

        public override void UnloadContent() {
            Objects.Clear();
            Content.Dispose();
        }
        public override void Dispose() {
            UnloadContent();
        }
    }
}
