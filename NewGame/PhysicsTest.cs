using System;
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
            
            World.ContactManager.OnBroadphaseCollision += penis;
        }

        void penis(ref FixtureProxy proxyA, ref FixtureProxy proxyB) {
            Logger.WriteLine($"{proxyA.ProxyId} {proxyB.ProxyId}");
        }

        Body floor;


        public override void LoadContent() {
            texture = new Texture2D(ServiceManager.GetService<GraphicsDevice>(), 25, 25);
            Utilities.FillTexture(ref texture, Color.Blue);

            var floorText = new Texture2D(ServiceManager.GetService<GraphicsDevice>(), 120, 120);
            Utilities.FillTexture(ref floorText, Color.Red);

            floor = BodyFactory.CreateRectangle(World, ConvertUnits.ToSimUnits(120), ConvertUnits.ToSimUnits(120), 1000f);
            floor.Position = ConvertUnits.ToSimUnits(Vector2.One * 200);
            floor.Awake = true;
            floor.SleepingAllowed = false;

            ServiceManager.Game.ExtraBeforeGameDraw += (sender, sb) => {
                sb.Draw(floorText, ConvertUnits.ToDisplayUnits(floor.Position), Color.White);
            };
        }

        public override void FixedUpdate(float? delta) {

                if (input.KeyPressed(Keys.A)) {
                    World.Enabled = !World.Enabled;
                }

                if (input.MouseLeftHeld) {
                    IPhysicsObject pp;  
                    PhysicsObjects.Add(pp = new PhysicsBody(World, texture, input.CurrentMouseState.PositionV));
                    pp.Body.SleepingAllowed = false;
                    Logger.WriteLine(PhysicsObjects.Count);
                }

            base.FixedUpdate(delta);
        }
    }
}
