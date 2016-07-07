using System;
using Animation;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Scenes;

namespace MonoGameTest.Entities {

    public abstract class BaseEntity : IEntity {
        public Vector2 Position { get; protected set; }
        public Rectangle BindingBox { get; protected set; }
        public ICamera Camera { get; protected set; }
        public Color Tint { get; set; }
        public float Health { get; protected set; }
        public bool Alive { get { return !(Health <= 0); } }
        public bool Spawned { get; protected set; }
        public Kind Kind { get; protected set; }

        public float Speed { get; set; }

        protected Texture2D charSheet { get; set; }
        protected static MyGame myGame { get; set; }

        protected AnimationSheet myAnimation { get; set; }
        protected KeyboardState pastKeyboardState { get; set; }
        protected MouseState pastMouseState { get; set; }

        protected delegate void EntitySpawn(TimeSpan timeSpawned, IEntity entity);

        protected delegate void EntityDespawn(TimeSpan timeDespawned, IEntity entity);

        protected event EntitySpawn OnSpawn;

        protected event EntityDespawn OnDespawn;

        protected BaseEntity(Kind kind = Kind.Neutral, float health = 100f, float speed = 200f) {
            Kind = kind;
            Health = Health;
            Speed = speed;
        }

        /// <summary>
        /// Handle Mouse Movment, by default it returns a <see cref="Vector2.Zero"/>
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns>Vector to add to <see cref="Position"/></returns>
        protected virtual Vector2 HandleMouseMovment(GameTime gameTime) {
            return Vector2.Zero;
        }

        /// <summary>
        /// Handle Keyboard Movment, by default it returns a <see cref="Vector2.Zero"/>
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns>Vector to add to <see cref="Position"/></returns>
        protected virtual Vector2 HandleKeyboardMovment(GameTime gameTime) {
            return Vector2.Zero;
        }

        /// <summary>
        /// Handles Keybinds and such, by default does nothing
        /// </summary>
        protected virtual void HandleControls() { }

        /// <summary>
        /// Gets called by <see cref="Update(GameTime)"/><para/>
        /// Has basic <see cref="Position"/>, <see cref="myAnimation"/>, and <see cref="BindingBox"/> update logic
        /// </summary>
        /// <param name="gameTime"></param>
        protected virtual void InternalUpdate(GameTime gameTime) {
            Camera.Update(gameTime);

            if (Spawned) {
                if (Alive) {
                    var velocity = (Mouse.GetState().LeftButton == ButtonState.Pressed ? HandleMouseMovment(gameTime) : HandleKeyboardMovment(gameTime));
                    Position += velocity;

                    if (velocity != Vector2.Zero) {
                        bool movingHorizontally = Math.Abs(velocity.X) > Math.Abs(velocity.Y);
                        if (movingHorizontally) {
                            if (velocity.X > 0) { //mouse right of player
                                myAnimation.SetState("walkRight"); //walkRight
                            } else { //mouse left of player
                                myAnimation.SetState("walkLeft"); //walkLeft
                            }
                        } else {
                            if (velocity.Y > 0) { // mouse below player
                                myAnimation.SetState("walkDown"); //walkDown
                            } else { //mouse above player
                                myAnimation.SetState("walkUp"); //walkUp
                            }
                        }
                    } else {
                        myAnimation.SetState("default");
                    }
                    HandleControls();
                } else {
                    myAnimation.SetState("death");
                }
            }
            myAnimation.Update(gameTime);
            BindingBox = new Rectangle(location: Position.ToPoint(), size: myAnimation.CurrentFrameBox.Size);
        }

        /// <summary>
        /// Gets called by <see cref="Draw(SpriteBatch)"/><para/>
        /// By default does basic spriteDrawing via <see cref="myAnimation"/> and <see cref="BindingBox"/>
        /// </summary>
        /// <param name="spriteBatch"></param>
        protected virtual void InternalDraw(SpriteBatch spriteBatch) {
            Camera.Draw(spriteBatch);
            if (Spawned) {
                spriteBatch.Draw(texture: charSheet, position: Position, sourceRectangle: myAnimation.CurrentFrameBox, layerDepth: SceneDepth.Entities);
                //spriteBatch.Draw(charSheet, Position, null, myAnimation.CurrentFrameBox, null, 0f, null, Tint, SpriteEffects.None, 0.1f);
            }

            if (myGame.DebugState) {
                var textPos = Position + new Vector2(0f, -20f);
                spriteBatch.DrawString(myGame.DebugFont, $"{Health} {Alive}, {Position} {myAnimation.CurrentState}",
                                       textPos, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, SceneDepth.DebugText);
            }
        }

        public void Spawn() {
            if (!Spawned)
                OnSpawn.Invoke(new TimeSpan(), this);
        }

        public void Despawn() {
            if (Spawned)
                OnDespawn.Invoke(new TimeSpan(), this);
        }

        public void Update(GameTime gameTime) {
            InternalUpdate(gameTime);
        }

        public void Draw(SpriteBatch sptBatch) {
            InternalDraw(sptBatch);
        }
    }
}