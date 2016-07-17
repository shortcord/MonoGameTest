using System;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShortCord.MonoGame.Physics.WorldItems {
    public class PhysicsObject : IPhysicsObject, IDisposable {
        public bool GameDrawEnabled { get; protected set; } = true;
        public bool UiDrawEnabled { get; protected set; } = false;

        public Texture2D Texture { get; protected set; }
        public Body Body { get; protected set; }

        public Vector2 Position => ConvertUnits.ToDisplayUnits(Body.Position);
        public float Rotation => ConvertUnits.ToDisplayUnits(Body.Rotation);

        public bool IsDisposed { get; private set; }

        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }

        public virtual void UiDraw(UiSpriteBatch spriteBatch) { }
        public void GameDraw(SpriteBatch spriteBatch) {
            if (!IsDisposed) {
                spriteBatch.Draw(
                    Texture,
                    position: Position,
                    rotation: Rotation,
                    color: Color.White
                );
            }
        }

        public void Dispose() {
            IsDisposed = true;
            Texture.Dispose();
            Body.Dispose();
        }
    }
}
