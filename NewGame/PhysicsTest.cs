﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using ShortCord.MonoGame;
using ShortCord.MonoGame.Physics;
using ShortCord.MonoGame.Components;
using ShortCord.MonoGame.Physics.WorldItems;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics;

namespace NewGame {
    public partial class PhysicsTest : PhysicsLevelObject {
        public PhysicsTest() : base() {
            UpdateEnabled = true;
            Details = new GameLevelDetails(1, "PhysicsTest");
        }

        Texture2D texture;
        Input input;


        public override void Start() {
            input = ServiceManager.GetService<Input>(); //get input manager
        }

        public override void LoadContent() {
            texture = new Texture2D(ServiceManager.GetService<GraphicsDevice>(), 25, 25);
            Utilities.FillTexture(ref texture, Color.Blue);

            var floorText = new Texture2D(ServiceManager.GetService<GraphicsDevice>(), 500, 120);
            Utilities.FillTexture(ref floorText, Color.Red);

            var floor = BodyFactory.CreateRectangle(World, ConvertUnits.ToSimUnits(floorText.Width), ConvertUnits.ToSimUnits(floorText.Height), 1f);
            floor.Position = ConvertUnits.ToSimUnits(new Vector2(floorText.Width, floorText.Height + floorText.Height));

            ServiceManager.Game.ExtraBeforeGameDraw += (sender, sb) => {
                sb.Draw(
                    texture: floorText,
                    position: ConvertUnits.ToDisplayUnits(floor.Position),
                    origin: floorText.Bounds.Center.ToVector2(),
                    color: Color.White);
            };
        }

        public override void FixedUpdate(float? delta) {
            
                if (input.KeyPressed(Keys.A)) {
                    World.Enabled = !World.Enabled;
                }

                if (input.MouseLeftClicked) {
                    PhysicsObject pp;
                    PhysicsObjects.Add(pp = new PhysicsBody(World, texture, input.CurrentMouseState.PositionV, 100f));
                    
                    Logger.WriteLine(PhysicsObjects.Count);
                }

            base.FixedUpdate(delta);
        }

        public override void Dispose() {
            base.Dispose();
        }
    }
}