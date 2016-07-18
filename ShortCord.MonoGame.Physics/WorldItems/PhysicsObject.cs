using System;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShortCord.MonoGame.Physics.WorldItems {
    public class PhysicsObject : IPhysicsObject {
        public bool GameDrawEnabled { get; protected set; } = true;
        public bool UiDrawEnabled { get; protected set; } = false;

        public Texture2D Texture { get; protected set; }
        public Vector2 Origin => Texture.Bounds.Center.ToVector2();
        public Body Body { get; protected set; }

        public float Rotation => Body.Rotation;
        public Vector2 Position => ConvertUnits.ToDisplayUnits(Body.Position);

        public bool IsDisposed { get; private set; }

        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }

        //TODO abstract the collision and other events
        public OnCollisionEventHandler OnPhysicsCollision;

        public virtual void UiDraw(UiSpriteBatch spriteBatch) { }
        public void GameDraw(SpriteBatch spriteBatch) {
            if (!IsDisposed) {
                spriteBatch.Draw(
                    Texture,
                    position: Position,
                    origin: Origin,
                    rotation: Rotation,
                    color: Color.White
                );
            }
        }
        
        public void Dispose() {
            IsDisposed = true;
            Body.OnCollision -= OnPhysicsCollision;
            Texture.Dispose();
            Body.Dispose();
        }

    }
}
