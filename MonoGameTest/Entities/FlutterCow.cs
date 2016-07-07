//
//  FlutterCow.cs
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
using Entities;
using Extensions;
using IO.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Scenes;

namespace MonoGameTest.Entities {

    public class FlutterCow : BaseEntity, IEntity {

        public FlutterCow(MyGame currentGame)
            : base(Kind.Player) {
            if (myGame == null) {
                myGame = currentGame;
            }

            if (charSheet == null) {
                charSheet = currentGame.Content.Load<Texture2D>("CowWalking");
            }

            Position = Vector2.Zero;
            Tint = Color.White;
            BindingBox = Rectangle.Empty;
            Speed = 200f;
            Health = 100f;
            myAnimation = new AnimationSheet(LoadAnimationFrames.FromJSON("Content/flutterCowAnim.json"));

            Camera = new PlayerCamera(myGame, this);

            OnSpawn += (time, entity) => {
                this.Spawned = true;
            };

            OnDespawn += (time, entity) => {
                this.Spawned = false;
            };
        }

        protected override void HandleControls() {
            var keyboard = Keyboard.GetState();
            var mouse = Mouse.GetState();

            if (keyboard.IsKeyDown(Keys.O) && !pastKeyboardState.IsKeyDown(Keys.O)) {
                myGame.DebugState = !myGame.DebugState;
            }

            if (keyboard.IsKeyDown(Keys.G) && !pastKeyboardState.IsKeyDown(Keys.G)) {
                Health = 0f;
            }

            if (keyboard.IsKeyDown(Keys.P) && !pastKeyboardState.IsKeyDown(Keys.P)) {
                myGame.gfxManger.PreferredBackBufferWidth = 1920;
                myGame.gfxManger.PreferredBackBufferHeight = 1080;
                myGame.gfxManger.ToggleFullScreen();
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
            pastKeyboardState = Keyboard.GetState();
            pastMouseState = Mouse.GetState();
        }

        protected override Vector2 HandleMouseMovment(GameTime gameTime) {
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

        protected override Vector2 HandleKeyboardMovment(GameTime gameTime) {
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
    }
}