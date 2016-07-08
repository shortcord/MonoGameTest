using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using ShortCord.MonoGame.Components;

namespace ShortCord.MonoGame.Ui {

    public struct UiObjectEventArgs {

        public UiObjectEventArgs(MouseState state, UiObject[] children = null) {
            MouseLocation = state.PositionV;
            Children = (children != null ? children : new UiObject[0]); //if children is null then give an empty array
        }

        public Vector2 MouseLocation;
        public UiObject[] Children;

        public static UiObjectEventArgs Empty {
            get { return new UiObjectEventArgs(); }
        }
    }

    public abstract class UiObject : GameObject, IUiElement {

        public event EventHandler<UiObjectEventArgs> Clicked;
        public event EventHandler<UiObjectEventArgs> MouseEntered;
        public event EventHandler<UiObjectEventArgs> MouseExited;

        public UiObject Parent { get; protected set; }
        public UiObject[] Children { get; protected set; }
        public string Name { get; protected set; }

        public Vector2 Postion { get; protected set; } = Vector2.Zero;
        public Color Color { get; protected set; } = Color.White;
        public Rectangle RenderRectangle { get; protected set; } = Rectangle.Empty;
        public Texture2D Texture { get; protected set; }
        protected bool isReady = false;

        protected UiObject() : base() {
            GameDrawEnabled = false; //Ui shouldnt be drawn on GameDraw

            FixedUpdateEnabled = true;
            UpdateEnabled = true;
            UiDrawEnabled = true;
        }

        protected void OnClicked(object sender, UiObjectEventArgs args) {
            Clicked?.Invoke(sender, args);
        }

        protected void OnMouseEntered(object sender, UiObjectEventArgs args) {
            MouseEntered?.Invoke(sender, args);
        }

        protected void OnMouseExited(object sender, UiObjectEventArgs args) {
            MouseExited?.Invoke(sender, args);
        }

        public override void UiDraw(UiSpriteBatch spriteBatch) {
            if (isReady)
                spriteBatch.Draw(Texture, RenderRectangle, Color);
        }
    }
}
