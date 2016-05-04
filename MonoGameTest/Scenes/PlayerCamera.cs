//
//  PlayerCamera.cs
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

using Entities;
using Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameTest;

namespace Scenes {

    public class PlayerCamera : ICamera {
        public Vector2 Position { get; private set; }
        public float Zoom { get; private set; }
        public float Rotation { get; private set; }

        public IEntity ControllingEntity {
            get {
                return controllingEntity;
            }

            private set {
                controllingEntity = value;
                Position = WorldToScreen(value.Position);
            }
        }

        private IEntity controllingEntity;

        private MyGame myGame { get; set; }

        private float interpPos { get; set; }
        private Vector2 lastPos { get; set; }
        private Vector2 targetPos { get; set; }

        public Rectangle ViewportWorldBoundry {
            get {
                Matrix inverseTransformation = Matrix.Invert(TransformationMatrix);
                Vector2 topLeft = Vector2.Transform(Vector2.Zero, inverseTransformation);
                Vector2 bottomRight = Vector2.Transform(new Vector2(myGame.GraphicsDevice.Viewport.Width, myGame.GraphicsDevice.Viewport.Height), inverseTransformation);

                return new Rectangle(topLeft.ToPoint(), (bottomRight - topLeft).ToPoint());
            }
        }

        public Matrix TransformationMatrix {
            get {
                return Matrix.CreateTranslation(-(int)Position.X, -(int)Position.Y, 0) *
                             Matrix.CreateRotationZ(Rotation) *
                             Matrix.CreateScale(new Vector3(Zoom, Zoom, 1f)) *
                             Matrix.CreateTranslation(myGame.GraphicsDevice.Viewport.Width * .5f,
                                                      myGame.GraphicsDevice.Viewport.Height * .5f, 0f);
            }
        }

        public PlayerCamera(MyGame game, IEntity controlingEntity) {
            Zoom = 1f;
            Rotation = 0f;
            myGame = game;
            ControllingEntity = controlingEntity;
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition) {
            return Vector2.Transform(screenPosition, TransformationMatrix);
        }

        public Vector2 WorldToScreen(Vector2 worldPosition) {
            return Vector2.Transform(worldPosition, Matrix.Invert(TransformationMatrix));
        }

        private float delta { get; set; }
        private float currentVal { get; set; }
        private float mouseInterp { get; set; }

        public void Update(GameTime gameTime) {
            targetPos = ControllingEntity.Position + (Vector2.One * (124 / 2)); //temp to center the cam, based on w&h of sprite frame / 2

            if (Position != Vector2.Zero) {
                Position.Normalize();
            }

            if (Position != targetPos) {
                lastPos = Position;
                interpPos = 0f;
            }

            if (interpPos < 1f) {
                interpPos = MathHelper.Clamp(interpPos + .05f, 0f, 1f);
                Position = Vector2.Lerp(Position, targetPos, interpPos);
            }

            delta = MathHelper.Clamp((Mouse.GetState().ScrollWheelValue / 120), 1, 5) - currentVal;
            currentVal += delta;
            Zoom = MathHelper.Lerp(Zoom, currentVal, .2f);
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (myGame.DebugState) {
                spriteBatch.DrawString(myGame.DebugFont,
                                       $"Camera {Position}\n{interpPos} {lastPos} {targetPos} {currentVal} {Mouse.GetState().ScrollWheelValue} | {delta} ({Zoom})",
                                       ViewportWorldBoundry.TopLeft(),
                                       Color.Black, 0f, Vector2.Zero,
                                       1f, SpriteEffects.None,
                                       layerDepth: SceneDepth.DebugText);
            }
        }
    }
}