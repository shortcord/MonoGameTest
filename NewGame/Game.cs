using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using NewGame.UI;
using NewGame.Engine;

namespace NewGame {

    public class Game : Microsoft.Xna.Framework.Game {

        GraphicsDeviceManager graphicsDeviceManager;
        SpriteBatch spriteBatch;
        SpriteBatch uiSpriteBatch;

        Button btn;

        public Game() {
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            Window.AllowAltF4 = false;

            ServiceManager.Game = this;
            ServiceManager.AddService(new GraphicsDeviceManager(this));
            ServiceManager.AddService(Content); //ContentManager

            graphicsDeviceManager = ServiceManager.GetService<GraphicsDeviceManager>();
            graphicsDeviceManager.PreferredBackBufferWidth = 800;
            graphicsDeviceManager.PreferredBackBufferHeight = 600;
            graphicsDeviceManager.ApplyChanges();            
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            ServiceManager.AddService(GraphicsDevice);
            ServiceManager.AddService(new UiSpriteBatch(GraphicsDevice));
            ServiceManager.AddService(new SpriteBatch(GraphicsDevice));

            spriteBatch = ServiceManager.GetService<SpriteBatch>();
            uiSpriteBatch = ServiceManager.GetService<UiSpriteBatch>();

            btn = new Button();

            base.LoadContent();
        }


        protected override void UnloadContent() {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Pink);
            spriteBatch.Begin();
            btn.Draw(uiSpriteBatch);
            spriteBatch.End();
        }

        protected override void Dispose(bool disposing) {
            ServiceManager.Clear();
            base.Dispose(disposing);
        }

    }
}
