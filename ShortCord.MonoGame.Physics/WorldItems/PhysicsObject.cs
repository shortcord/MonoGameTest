using System;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShortCord.MonoGame.Physics.WorldItems {
    public class PhysicsObject : IPhysicsObject {
        public bool GameDrawEnabled { get; protected set; } = true;
        public bool UiDrawEnabled { get; protected set; } = false;
        public bool IsDisposed { get; private set; }

        public Texture2D Texture { get; protected set; }
        public Vector2 Origin => new Vector2(Texture.Width, Texture.Height) / 2f;
        public Vector2 Position => ConvertUnits.ToDisplayUnits(Body.Position);
        public float Rotation => Body.Rotation;

        public Body Body {
            get { return _body; }
            protected set {
                /* You shouldn't be able to reset the body 
                 * let alone need to remove the events this early
                 * but just incase we call it here
                 */
                if (Body != null) { //remove events if the body isnt null
                    Logger.WriteLine($"{this}[{GetHashCode()}] | Body isn't null, resetting events");
                    RemoveEvents();
                }

                _body = value;
                SetupEvents();
            }
        }
        Body _body;


        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }

        //TODO abstract the collision and other events
        protected OnCollisionEventHandler OnPhysicsCollision = delegate { return true; };
        protected OnSeparationEventHandler OnPhysicsSeparation = delegate { };

        protected void SetupEvents() {
            Body.OnCollision += OnPhysicsCollision;
            Body.OnSeparation += OnPhysicsSeparation;
        }

        protected void RemoveEvents() {
            Body.OnCollision -= OnPhysicsCollision;
            Body.OnSeparation -= OnPhysicsSeparation;
        }

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
            RemoveEvents(); //unsub events to make sure the GC can clean up
            Texture.Dispose();
            Body.Dispose();
        }

    }
}
