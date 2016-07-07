//
//  BlobEntity.cs
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
using IO.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameTest;
using Scenes;

namespace Entities {

    public class BlobEntity : IEntity {
        public Vector2 Position { get; private set; }
        public Rectangle BindingBox { get; private set; }
        public ICamera Camera { get { return null; } } //This entity doesnt have a camera nor should it ever
        public Color Tint { get; set; }
        public float Health { get; private set; }
        public bool Alive { get { return !(Health <= 0); } }
        public bool Spawned { get; private set; }
        public Kind Kind { get { return Kind.Hostile; } }

        private static Texture2D charSheet { get; set; }
        private static MyGame myGame { get; set; }
        private static float Speed { get; set; }
        private static Random ran { get; set; }

        private AnimationSheet myAnimation { get; set; }

        public BlobEntity(MyGame currentGame) {
            if (myGame == null) {
                myGame = currentGame;
            }

            if (charSheet == null) {
                charSheet = currentGame.Content.Load<Texture2D>("blob");
            }
            if (ran == null) {
                ran = new Random();
            }

            Position = new Vector2(5f, 5f) * ran.Next(2, 20);
            Tint = Color.White;
            BindingBox = Rectangle.Empty;

            Spawn();

            Health = 25f;
            Speed = 120f;
            myAnimation = new AnimationSheet(LoadAnimationFrames.FromJSON("Content/blobAnim.json"));
        }

        private Vector2 FollowTarget(IEntity target, GameTime gameTime) {
            var toReturn = Vector2.Zero;

            toReturn = target.Position - Position;
            if (Math.Abs(toReturn.X) > 0 || Math.Abs(toReturn.Y) > 0) {
                toReturn.Normalize();
            }

            toReturn *= Speed;
            toReturn *= (float)gameTime.ElapsedGameTime.TotalSeconds;
            //return (myGame.Player.Alive ? toReturn : Vector2.Zero);
            return Vector2.Zero;
        }

        public void Despawn() {
            Spawned = false;
        }

        public void Spawn() {
            Spawned = true;
        }

        public void Update(GameTime gameTime) {
            Vector2 velocity = Vector2.Zero;
            if (Spawned) {
                //velocity = FollowTarget(myGame.Player, gameTime);
                velocity = Vector2.Zero;
                Position += velocity;
            }

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
            myAnimation.Update(gameTime);
            BindingBox = new Rectangle(location: Position.ToPoint(), size: myAnimation.CurrentFrameBox.Size);
        }

        public void Draw(SpriteBatch spriteBatch) {
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

                if (myGame.DebugState) {
                    var textPos = Position + new Vector2(0f, -20f);
                    spriteBatch.DrawString(myGame.DebugFont, $"{Health} {Alive}, {Position} {myAnimation.CurrentState}",
                                           textPos, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, SceneDepth.DebugText);
                }
            }
        }
    }
}