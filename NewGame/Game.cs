using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NewGame.UI;
using ShortCord.MonoGame;
using ShortCord.MonoGame.Extensions;

namespace NewGame {

    public class Game : GameWrapper {

        Input input;

        public Game() : base("SC Game Test") {
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.AllowAltF4 = false;
            CurrentLevel = new MainMenu();

        }


        protected override void Initialize() {
            GameComponents.Add(input = new Input());
            
            input.KeyPressedEvent += Input_KeyPressedEvent;
            input.MouseStateChanged += Input_MouseStateChanged;

            base.Initialize();
        }

        void Input_MouseStateChanged(object sender, MouseState e) {
            Console.WriteLine(e.PositionV);
        }

        void Input_KeyPressedEvent(object sender, Keys[] e) {
            if (e.Contains(Keys.Escape)) {
                this.Exit();
            }
        }
        protected override void LoadContent() {
            base.LoadContent();
        }
    }
}
