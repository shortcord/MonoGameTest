using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShortCord.MonoGame;
using ShortCord.MonoGame.Components;

namespace NewGame.UI {
    public class Button : GameObject {

        public Rectangle RenderRectangle {
            get { return renderRectangle; }
            private set { renderRectangle = value; }
        }
        Rectangle renderRectangle;

        Texture2D texture;

        volatile bool isReady;

        public Button() : base() {
            UiDrawEnabled = true;
        }

        public override void LoadContent() {
            RenderRectangle = new Rectangle(5, 5, 80, 40);
            texture = new Texture2D(ServiceManager.GetService<GraphicsDevice>(), 20, 60);
            Color[] colorData = new Color[RenderRectangle.Width * RenderRectangle.Height];
            for (int i = 0; i < colorData.Length; i++) {
                colorData[i] = Color.White;
            }
            texture.SetData(colorData);

            isReady = true;
        }

        public override void UiDraw(UiSpriteBatch spriteBatch) {
            if (isReady)
                spriteBatch.Draw(texture, renderRectangle, Color.White);            
        }
    }
}
