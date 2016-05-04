//
//  Game.cs
//
//  Author:
//       Tristan <tristan@shortcord.com>
//
//  Copyright (c) 2016 Tristan Smith
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scenes;

namespace MonoGameTest {

    public class MyGame : Game {
        private GraphicsDeviceManager gfxManger;
        private SpriteBatch spriteBatch;
        private IReadOnlyDictionary<string, IScene> Scenes { get; set; }
        private IScene currentScene { get; set; }

        public SpriteFont DebugFont { get; private set; }
        public bool DebugState { get; set; }

        public MyGame() {
            IsMouseVisible = true;
            gfxManger = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            Window.AllowAltF4 = true;
#if DEBUG
            DebugState = true;
#endif
            gfxManger.PreferredBackBufferWidth = 1600;
            gfxManger.PreferredBackBufferHeight = 800;
            gfxManger.ApplyChanges();

            Window.ClientSizeChanged += WindowSizeChanged;

            Scenes = new Dictionary<string, IScene>() {
                {"testlevel", new TestLevel(this)},
            };
            currentScene = Scenes["testlevel"];
        }

        protected override void Initialize() {
            currentScene.Load();
            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            DebugFont = Content.Load<SpriteFont>("Font");

            base.LoadContent();
        }

        protected override void UnloadContent() {
            Content.Unload();
            base.UnloadContent();
        }

        protected void WindowSizeChanged(object sender, EventArgs args) {
#if DEBUG
            Console.WriteLine($"Client called window resize event, new size is {GraphicsDevice.Viewport.Bounds.Size}");
#endif
        }

        protected override void Update(GameTime gameTime) {
            currentScene.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront, transformMatrix: currentScene.PlayerCamera.TransformationMatrix);
            currentScene.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}