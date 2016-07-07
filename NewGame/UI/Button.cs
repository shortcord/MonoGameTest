using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NewGame.UI {
    public class Button : IUiItem {

        public event EventHandler OnClick;

        public Rectangle RenderRectangle {
            get { return renderRectangle; }
            private set { renderRectangle = value; }
        }
        Rectangle renderRectangle;

        Texture2D texture;

        public Button() {
            RenderRectangle = new Rectangle(5, 5, 80, 40);
            texture = new Texture2D(ServiceManager.GetService<GraphicsDevice>(), 20, 60);
            Color[] colorData = new Color[RenderRectangle.Width * RenderRectangle.Height];
            for (int i = 0; i < colorData.Length; i++) {
                colorData[i] = Color.White;
            }
            texture.SetData(colorData);
        }


        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, renderRectangle, Color.White);
        }

        public void Update(float deltaTime) {
            
        }
    }
}
