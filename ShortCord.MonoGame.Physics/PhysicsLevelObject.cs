using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShortCord.MonoGame.Camera;
using ShortCord.MonoGame.Components;
using ShortCord.MonoGame.Physics.WorldItems;

namespace ShortCord.MonoGame.Physics {
    public class PhysicsLevelObject : LevelObject {

        protected List<IPhysicsObject> PhysicsObjects;
        protected LevelCamera Camera { get; }

        public World World { get; protected set; }

        public PhysicsLevelObject(Vector2? gravity = null) : base() {
            FixedUpdateEnabled = true;
            GameDrawEnabled = true;

            PhysicsObjects = new List<IPhysicsObject>();

            World = new World(gravity ?? new Vector2(0f, 9.82f));

            Camera = ServiceManager.GetService<LevelCamera>();
        }

        public override void FixedUpdate(float? delta) {
            World.Step((float)delta);
        }

        public override void GameDraw(SpriteBatch spriteBatch, bool debugDraw) {
            IPhysicsObject[] tempArray = PhysicsObjects.ToArray();
            for (int i = 0; i < tempArray.Length; i++) {

                if (tempArray[i].AutoCleanUp && !Camera.VisibleArea.Contains(tempArray[i].Position)) { //TODO add a 'padding' effect so objects don't just dispose
                    PhysicsObjects.Remove(tempArray[i]);
                    tempArray[i].Dispose();
                    continue;
                }

                if (tempArray[i].GameDrawEnabled) {
                    tempArray[i].GameDraw(spriteBatch, Debug);
                }
            }
        }

        public override void Dispose() {
            IPhysicsObject[] tempArray = PhysicsObjects.ToArray();
            for (int i = 0; i < tempArray.Length; i++) {
                    tempArray[i].Body.Dispose();
            }
            base.Dispose();
        }
    }
}
