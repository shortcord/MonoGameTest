using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShortCord.MonoGame.Physics.WorldItems {

    public class PhysicsBody : PhysicsObject {
        public PhysicsBody(World world, Texture2D texture, Vector2? position = null, float rotation = 0f) : base() {
            Body = BodyFactory.CreateBody(world, ConvertUnits.ToSimUnits(position ?? Vector2.Zero), rotation);
            Body.BodyType = BodyType.Dynamic;
            Texture = texture;
        }
    }
}