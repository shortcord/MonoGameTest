using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShortCord.MonoGame.Components;

namespace ShortCord.MonoGame {
    public class Actor : GameObject {

        public Texture2D Texture { get; protected set; }

        public Vector2 Position { get; protected set; } = Vector2.Zero;

        public float Layer { get; protected set; } = 0f;

        public Actor(Texture2D texture) {
            this.Texture = texture;

            base.GameDrawEnabled = true;
            base.UpdateEnabled = true;
            base.FixedUpdateEnabled = true;
        }

    }
}
