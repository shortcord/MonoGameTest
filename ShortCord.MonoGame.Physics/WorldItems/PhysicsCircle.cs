using System;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShortCord.MonoGame.Physics.WorldItems {
    public class PhysicsCircle : PhysicsObject {
        //float radius, float density
        public PhysicsCircle(World world, Texture2D texture, float radius = 0.5f, float density = 0f, float rotation = 0f) : base() {
            Body = BodyFactory.CreateCircle(world, radius, density);
            Body.Rotation = rotation;
            Body.BodyType = BodyType.Dynamic;
            Sprite = new PhysicsSpriteDefinition(this, texture);
        }
    }
}
