using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShortCord.MonoGame.Physics.WorldItems;

namespace ShortCord.MonoGame.Physics {
    /// <summary>
    /// A Definition of a 2D Physics Sprite
    /// </summary>
    public class PhysicsSpriteDefinition : Graphics.SpriteDefinitionBase {

        public IPhysicsObject Body { get; }
        public override float Rotation { get { return Body.Rotation; } set { Body.Rotation = value; } }
        public override Vector2 Position { get { return Body.Position; } set { Body.Position = value; } }

        /// <summary>
        /// Create a new Instantce of <see cref="PhysicsSpriteDefinition"/>
        /// </summary>
        /// <param name="body"><see cref="IPhysicsObject"/> of Sprite</param>
        /// <param name="position"><see cref="Position"/> of Sprite</param>
        /// <param name="mask"><see cref="Mask"/> of Sprite</param>
        public PhysicsSpriteDefinition(IPhysicsObject body, Texture2D texture, Color? mask = null) {
            Texture = texture;
            Mask = mask ?? Color.White;
            Body = body;
        }
    }
}
