using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShortCord.MonoGame;
using ShortCord.MonoGame.Extensions;

namespace NewGame {

    public class Game : GameWrapper {

        Input input;

        public Game() : base("SC Game Test") {
            CurrentLevelChanged += (sender, e) => {
                Logger.WriteLine($"Level Changed {e?.ID} {e?.FriendlyName}");
            };
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.AllowAltF4 = false;

            Levels.Add("MainMenu", new MainMenu());
            Levels.Add("PhysicsTest", new PhysicsTest());

            CurrentLevel = Levels["PhysicsTest"];
        }

        protected override void Initialize() {
            GameComponents.Add(input = new Input());
            ServiceManager.AddService(input);

            input.KeyPressedEvent += Input_KeyPressedEvent;

            base.Initialize();
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
