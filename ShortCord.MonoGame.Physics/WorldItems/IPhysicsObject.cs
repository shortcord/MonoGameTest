using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using ShortCord.MonoGame.Components;

namespace ShortCord.MonoGame.Physics.WorldItems {
    public interface IPhysicsObject : IDrawable {
        Body Body { get; }
    }
}
