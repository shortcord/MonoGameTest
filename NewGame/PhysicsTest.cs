using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using ShortCord.MonoGame;
using ShortCord.MonoGame.Camera;
using ShortCord.MonoGame.Physics;
using ShortCord.MonoGame.Components;
using ShortCord.MonoGame.Physics.WorldItems;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics;
using ShortCord.MonoGame.Graphics;

namespace NewGame {
    public partial class PhysicsTest : PhysicsLevelObject {
        public PhysicsTest() : base() {
            UpdateEnabled = true;
            UiDrawEnabled = true;
            Details = new GameLevelDetails(1, "PhysicsTest");
            Debug = true;
        }

        Texture2D texture;
        Input input;

        public override void Start() {
            input = ServiceManager.GetService<Input>(); //get input manager
        }

        IPhysicsObject floor;

        public override void LoadContent() {
            texture = new Texture2D(ServiceManager.GetService<GraphicsDevice>(), 25, 25);
            Utilities.FillTexture(ref texture, Color.Blue);

            var floorText = new Texture2D(ServiceManager.GetService<GraphicsDevice>(), 500, 120);
            Utilities.FillTexture(ref floorText, Color.Orange);

            floor = new PhysicsBody(World, floorText, new Vector2(floorText.Width, floorText.Height * 2));
            floor.Body.BodyType = BodyType.Static;

            var floorSprite = new PhysicsSpriteDefinition(floor, floorText);

            ServiceManager.Game.ExtraBeforeGameDraw += (sender, sb) => {
                floorSprite.Draw(sb, Debug);
            };
        }
        
        public override void FixedUpdate(float? delta) {
            if (input.KeyPressed(Keys.P)) {
                World.Enabled = !World.Enabled;
            }

            if (input.KeyPressed(Keys.OemMinus)) {
                Debug = !Debug;
                Logger.WriteLine($"[{this}] Debug State changed to {Debug}");
            }

            if (input.MouseRightHeld) {
                IPhysicsObject[] phsysicsObjects = PhysicsObjects.ToArray();
                foreach(var phyObj in phsysicsObjects) {
                    phyObj.Body.ApplyForce(ConvertUnits.ToSimUnits(Vector2.Right * 15));
                }
            }

            if (input.MouseLeftHeld) {
                PhysicsObjects.Add(new PhysicsBody(World, texture, ServiceManager.GetService<LevelCamera>().StWmousePos));
                Logger.WriteLine(PhysicsObjects.Count);
            }

            if (input.KeyPressed(Keys.Space)) {
                floor.Position += Vector2.Right * 5;
            }

            base.FixedUpdate(delta);
        }

        public override void Dispose() {
            //camera.Dispose();
            base.Dispose();
        }
    }
}
