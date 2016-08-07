using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShortCord.MonoGame.Physics.WorldItems {
    public interface IPhysicsObject : Components.IDrawable, IDisposable {
        float Rotation { get; set; }
        Vector2 Position { get; set; }
        Body Body { get; }
        bool AutoCleanUp { get; }
    }
}
