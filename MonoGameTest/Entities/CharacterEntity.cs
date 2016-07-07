//
//  CharacterEntity.cs
//
//  Author:
//       Tristan <tristan@shortcord.com>
//
//  Copyright (c) 2016 Tristan Smith
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using Animation;
using Extensions;
using IO.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameTest;
using Scenes;

namespace Entities {

    public class CharacterEntity : IEntity {
        public Vector2 Position { get; private set; }
        public Rectangle BindingBox { get; private set; }
        public ICamera Camera { get; private set; }
        public Color Tint { get; set; }
        public float Health { get; private set; }
        public bool Alive { get { return !(Health <= 0); } }
        public bool Spawned { get; private set; }
        public Kind Kind { get { return Kind.Player; } }

        public float Speed { get; set; }

        private static Texture2D charSheet { get; set; }
        private static MyGame myGame { get; set; }

        private AnimationSheet myAnimation { get; set; }
        private KeyboardState pastKeyboardState { get; set; }
        private MouseState pastMouseState { get; set; }

        public CharacterEntity(MyGame currentGame) {
            if (myGame == null) {
                myGame = currentGame;
            }

            if (charSheet == null) {
                charSheet = currentGame.Content.Load<Texture2D>("player");
            }

            Position = Vector2.Zero;
            Tint = Color.White;
            BindingBox = Rectangle.Empty;
            Speed = 200f;
            Health = 100f;
            myAnimation = new AnimationSheet(LoadAnimationFrames.FromJSON("Content/playerAnim.json"));

            Camera = new PlayerCamera(myGame, this);
        }

        #region Controls

        private Vector2 HandleMouseMovment(GameTime gameTime) {
            var mouse = Mouse.GetState();
            var toReturn = Vector2.Zero;
            if (mouse.LeftButton == ButtonState.Pressed && mouse.In(myGame.GraphicsDevice) && myGame.IsActive) {
                toReturn.X = mouse.X - Position.X;
                toReturn.Y = mouse.Y - Position.Y;
                toReturn = Camera.WorldToScreen(toReturn);
                if (Math.Abs(toReturn.X) > 0 || Math.Abs(toReturn.Y) > 0) {
                    toReturn.Normalize();
                }
                toReturn *= Speed;
                toReturn *= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            return toReturn;
        }

        private Vector2 HandleKeyboardMovment(GameTime gameTime) {
            if (myGame.IsActive && Mouse.GetState().In(myGame.GraphicsDevice)) {
                var keyboard = Keyboard.GetState();
                var toReturn = Vector2.Zero;
                if (keyboard.IsKeyDown(Keys.W)) {
                    if (!keyboard.IsKeyDown(Keys.S)) {
                        toReturn += Vector2.Up * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                } else if (keyboard.IsKeyDown(Keys.S)) {
                    toReturn += Vector2.Down * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (keyboard.IsKeyDown(Keys.A)) {
                    if (!keyboard.IsKeyDown(Keys.D)) {
                        toReturn += Vector2.Left * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                } else if (keyboard.IsKeyDown(Keys.D)) {
                    toReturn += Vector2.Right * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.S)) {
                    if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.A)) {
                        toReturn *= 0.8f;
                    }
                }
                return toReturn;
            } else {
                return Vector2.Zero;
            }
        }

        private void HandleControls() {
            var keyboard = Keyboard.GetState();
            var mouse = Mouse.GetState();

            if (keyboard.IsKeyDown(Keys.O) && !pastKeyboardState.IsKeyDown(Keys.O)) {
                myGame.DebugState = !myGame.DebugState;
            }

            if (keyboard.IsKeyDown(Keys.G) && !pastKeyboardState.IsKeyDown(Keys.G)) {
                Health = 0f;
            }

            if ((mouse.RightButton == ButtonState.Pressed)) {
                if (keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.RightShift)) {
                    for (int i = 0; i <= 5; i++) {
                        //myGame.Entities.Add(new BlobEntity(myGame));
                    }
                } else {
                    //myGame.Entities.Add(new BlobEntity(myGame));
                }
            }
#if DEBUG
            if (keyboard.IsKeyDown(Keys.Escape) || keyboard.IsKeyDown(Keys.OemTilde)) {
#else
			if (keyboard.IsKeyDown(Keys.Escape)) {
#endif
                myGame.Exit();
            }

            pastKeyboardState = keyboard;
            pastMouseState = mouse;
        }

        #endregion Controls

        public void Despawn() {
            if (Spawned) {
                Spawned = false;
            }
        }

        public void Spawn() {
            if (!Spawned) {
                Spawned = true;
            }
        }

        private TimeSpan lastHit { get; set; }

        public void Update(GameTime gameTime) {
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

            /*
			foreach (IEntity entity in myGame.Entities) {
				if (entity.Kind != (Kind.Frendly | Kind.Player)) {
					if (BindingBox.Intersects(entity.BindingBox)) {
						if (entity.Kind == Kind.Hostile) {
							if (lastHit != gameTime.TotalGameTime + TimeSpan.FromSeconds(20)) {
								lastHit = gameTime.TotalGameTime;
							}
							if (gameTime.TotalGameTime >= lastHit) {
								Health -= 2.5f;
								lastHit = gameTime.TotalGameTime + TimeSpan.FromSeconds(20);
							}
						}
					}
				}
			}
			*/

            // TODO add health regen
            /*
			if (gameTime.TotalGameTime >= lastHit && (Health < 90f) && Alive) {
				Health += 0.3f;
			}
			*/
        }

        public void Draw(SpriteBatch spriteBatch) {
            Camera.Draw(spriteBatch);

            /* The Draw call lets me sort when sprites get drawn if SpriteBatch is rendering in SpriteSortMode.BackToFront
			 * Texture2D texture,
			 * Vector2? position = null,
			 * Rectangle? destinationRectangle = null,
			 * Rectangle? sourceRectangle = null, <! Useful for Animation
			 * Vector2? origin = null,
			 * float rotation = 0f,
			 * Vector2? scale = null,
			 * Color? color = null, <! Tint
			 * SpriteEffects effects = SpriteEffects.None,
			 * float layerDepth = 0f <! Layer to draw to
			 */
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
    }
}