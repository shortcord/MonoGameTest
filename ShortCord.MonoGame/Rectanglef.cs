using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShortCord.MonoGame {
    public struct Rectanglef {
        float _width, _height, _X, _Y;

        public float Width {
            get { return _width; }
            set { _width = value; }
        }

        public float Height {
            get { return _height; }
            set { _height = value; }
        }

        public float X {
            get { return _X; }
            set { _X = value; }
        }

        public float Y {
            get { return _Y; }
            set { _Y = value; }
        }

        public Vector2 Location {
            get { return new Vector2(X, Y); }
            set { X = value.X; Y = value.Y; }
        }

        public Vector2 Size {
            get { return new Vector2(Width, Height); }
        }

        public Rectanglef(Vector2 Position, Vector2 Size) {
            _width = Size.X;
            _height = Size.Y;
            _X = Position.X;
            _Y = Position.Y;
        }

        public Rectangle ToRectangle() {
            return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
        }

        static public Rectangle ToRectangle(Rectanglef rec) {
            return rec.ToRectangle();
        }

        static public bool operator ==(Rectanglef rec1, Rectanglef rec2) {
            return ((rec1.Width == rec2.Height) && (rec1.Width == rec2.Width) && (rec1.X == rec2.X) && (rec1.Y == rec2.Y));
        }

        static public bool operator !=(Rectanglef rec1, Rectanglef rec2) {
            return !(rec1 == rec2);
        }

        public override string ToString() {
            return $"Rectanglef {{{_width}, {_height}, {_X}, {_Y}}}";
        }

    }
}
