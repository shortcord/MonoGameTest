using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShortCord.MonoGame.Ui {
    public class Label : UiObject {
        SpriteFont _font;
        Point? MaxLength;

        Texture2D debug;

        public Label(string name = "New Label", Point? maxLength = null, Point? position = null) 
            : base(null, position) {
            Name = name;
            Color = Color.White;
            MaxLength = maxLength;
        }

        public override void LoadContent() {
            _font = ServiceManager.GetService<ContentManager>().Load<SpriteFont>("Font");
            var stringLength = _font.MeasureString(Name).ToPoint();

            if (MaxLength != null && stringLength.X > MaxLength.Value.X) {
                string newName = Name;
                do {
                    newName = newName.Remove(newName.Length - 1, 1);
                    stringLength = _font.MeasureString(newName).ToPoint();
                } while (stringLength.X > MaxLength.Value.X);

                newName = newName.Remove(newName.Length - 3);
                newName = newName + "...";
                Name = newName;
            }

            var gfxD = ServiceManager.GetService<GraphicsDevice>();
            RenderTarget2D target = new RenderTarget2D(gfxD, stringLength.X, stringLength.Y);
            var batch = new SpriteBatch(gfxD);
            gfxD.SetRenderTarget(target);
            gfxD.Clear(Color.Transparent);
            batch.Begin();
            batch.DrawString(_font, Name, Vector2.Zero, Color.Black);
            batch.End();
            gfxD.SetRenderTarget(null);
            Texture = target;

            debug = new Texture2D(ServiceManager.GetService<GraphicsDevice>(), 5, 5);
            Color[] colorData = new Color[5*5];
            for (int i = 0; i < colorData.Length; i++) {
                colorData[i] = Color.Red;
            }
            debug.SetData(colorData);

            RenderRectangle = new Rectangle(RenderRectangle.Location, new Point(Texture.Width, Texture.Height));
            //Origin = RenderRectangle.Location.ToVector2() / 2;
            isReady = true;
        }

        public override void UiDraw(UiSpriteBatch spriteBatch) {
            if (isReady) {
                spriteBatch.Draw(
                    texture: debug, 
                    position: RenderRectangle.Location.ToVector2(), 
                    color: Color.White, 
                    //origin: RenderRectangle.Location.ToVector2() / 2,
                    layerDepth: 1f);
            }
            base.UiDraw(spriteBatch);
        }

        public override void UnloadContent() {
            _font.Texture.Dispose();
        }
    }
}
