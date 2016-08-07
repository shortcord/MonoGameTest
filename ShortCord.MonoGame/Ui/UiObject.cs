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

        public virtual UiObject Parent { get; protected set; }
        public virtual UiObject[] Children { get; protected set; } = new UiObject[0];
        public string Name { get; protected set; }

        //public Vector3 Postion { get; protected set; } = Vector3.Zero;
        public Vector2 Origin { get; protected set; } = Vector2.Zero;
        public Color Color { get; protected set; } = Color.White;
        public Rectangle RenderRectangle { get; set; } = Rectangle.Empty;
        public Texture2D Texture { get; protected set; }
        public float Rotation { get; set; } = 0f;
        public float Layer { get; set; } = 0f;
        protected bool isReady = false;

        protected UiObject(Point? size = null, Point? position = null) : base() {

            RenderRectangle = new Rectangle(position ?? new Point(5), size ?? new Point(180));

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

        public override void Start() {
            for (int i = 0; i < Children.Length; i++) {
                Children[i].Start();
            }
        }

        public override void LoadContent() {
            for (int i = 0; i < Children.Length; i++) {
                if(!Children[i].isReady) //we can call any child's .LoadContent() prematurely; so we check before recalling
                    Children[i].LoadContent();
            }
        }

        public override void UnloadContent() {
            for (int i = 0; i < Children.Length; i++) {
                Children[i].UnloadContent();
            }
        }

        public override void FixedUpdate(float? delta) {
            for (int i = 0; i < Children.Length; i++) {
                if (Children[i].FixedUpdateEnabled) {
                    Children[i].FixedUpdate(delta);
                }
            }
        }

        public override void Update(float delta) {
            for (int i = 0; i < Children.Length; i++) {
                if (Children[i].UpdateEnabled) {
                    Children[i].Update(delta);
                }
            }
        }

        public override void UiDraw(UiSpriteBatch spriteBatch, bool debugDraw) {
            if (isReady && UiDrawEnabled) {
                for (int i = 0; i < Children.Length; i++) {
                    var child = Children[i];
                    if (child.UiDrawEnabled) {

                        child.UiDraw(spriteBatch, debugDraw);

                        spriteBatch.Draw(
                            texture: child.Texture, 
                            destinationRectangle: child.RenderRectangle, 
                            color: child.Color, 
                            rotation: child.Rotation, 
                            origin: child.Origin, 
                            effects: SpriteEffects.None, 
                            layerDepth: child.Layer);
                    }
                }
                spriteBatch.Draw(
                            texture: Texture,
                            destinationRectangle: RenderRectangle,
                            color: Color,
                            rotation: Rotation,
                            origin: Origin,
                            effects: SpriteEffects.None,
                            layerDepth: Layer);
            }
        }
    }
}
