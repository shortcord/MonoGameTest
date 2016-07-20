using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ShortCord.MonoGame.Components;

namespace ShortCord.MonoGame.Camera {
    public interface ICamera : Components.IDrawable, IUpdatable, IDisposable {
        Vector2 Position { get; }
        Vector2 Size { get; }
        Vector2 Origin { get; }
        float Zoom { get; }
        float Rotation { get; }

        Rectangle VisibleArea { get; }
        Matrix TransformationMatrix { get; }

        Vector2 ScreenToWorld(Vector2 mouseLocation);
        Vector2 WorldToScreen(Vector2 mouseLocation);
    }
}
