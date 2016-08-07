using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ShortCord.MonoGame.Ui {
    public class Button : UiObject {

        public Button(
            string name = "New Button", 
            bool fitToText = false, 
            Point? size = null, 
            Point? position = null) 
            : base(size, position) {
            _fitToText = fitToText;
            Name = name;
        }

        Input input;
        Label _buttonText;
        bool _fitToText;

        List<UiObject> children;

        public override void Start() {
            input = ServiceManager.GetService<Input>();
            children = new List<UiObject>();

            _buttonText = new Label(Name, RenderRectangle.Size, RenderRectangle.Location);
            Layer = 0.1f;
            _buttonText.Layer = Layer + 0.1f;

            children.Add(_buttonText);

            Children = children.ToArray();

            base.Start();
        }

        public override void LoadContent() {
            if (_fitToText) {
                _buttonText.LoadContent(); //call the label's LoadContent early so we can get the render rectangle
                RenderRectangle = new Rectangle(RenderRectangle.Location, _buttonText.RenderRectangle.Size);
            }

            Texture = new Texture2D(ServiceManager.GetService<GraphicsDevice>(), RenderRectangle.Size);
            Color[] colorData = new Color[RenderRectangle.Width * RenderRectangle.Height];
            for (int i = 0; i < colorData.Length; i++) {
                colorData[i] = Color.White;
            }
            Texture.SetData(colorData);

            base.LoadContent();

            isReady = true;
        }

        public override void UiDraw(UiSpriteBatch spriteBatch, bool debugDraw) {
            base.UiDraw(spriteBatch, debugDraw);
        }

        bool mouseEntered;
        public override void FixedUpdate(float? delta) {
            var currentMouse = input.CurrentMouseState;

            if (!mouseEntered && RenderRectangle.Contains(currentMouse.Position)) {
                /* we check of the mouse as already left to keep from calling the event more than once
                 * then we check if the current mouse position is within the rectangle
                 * if above is true then we invoke the entered event
                 */
                OnMouseEntered(this, new UiObjectEventArgs(currentMouse));
                mouseEntered = true;
            } else if (mouseEntered && !RenderRectangle.Contains(currentMouse.Position)) {
                /* we check if the mouse is currently in the rectangle AND the rectangle doesnt currently contain the mouse position
                 * we invoke the exit event
                 */
                OnMouseExited(this, new UiObjectEventArgs(currentMouse));
                mouseEntered = false;
            }

            if (mouseEntered && input.MouseLeftClicked) {
                /* we check if the mouse is currently in the retangle
                 * and if the mouse left button is clicked
                 * we invoke the clicked event
                 */
                OnClicked(this, new UiObjectEventArgs(currentMouse));
            }
        }
    }
}
