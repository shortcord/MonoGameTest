using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShortCord.MonoGame.Physics.WorldItems {

    public class PhysicsBody : PhysicsObject {
        public PhysicsBody(World world, Texture2D texture, Vector2? position = null, float density = 1f, float rotation = 0f) : base() {
            Body = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(texture.Width), ConvertUnits.ToSimUnits(texture.Height), density, rotation);
            Body.Position = ConvertUnits.ToSimUnits(position ?? Vector2.Zero);
            Body.BodyType = BodyType.Dynamic;
            Body.OnCollision += base.OnPhysicsCollision;
            Texture = texture;
        }
    }
}