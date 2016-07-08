using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using ShortCord.MonoGame;
using ShortCord.MonoGame.Ui;
using ShortCord.MonoGame.Components;

namespace NewGame {
    internal class MainMenu : LevelObject {

        Button btn;
        
        public MainMenu() : base() {
            FixedUpdateEnabled = true;
            UpdateEnabled = true;
            UiDrawEnabled = true;
            GameDrawEnabled = true;
        }

        public override void Start() {
            btn = new Button("Example Button");
            btn.Start();
            btn.Clicked += (sender, args) => {
                Console.WriteLine($"[{((Button)sender).Name}] clicked :D");
            };
            btn.MouseEntered += (sender, args) => {
                Console.WriteLine($"[{((Button)sender).Name}] mouse entered!");
            };
            btn.MouseExited += (sender, args) => {
                Console.WriteLine($"[{((Button)sender).Name}] mouse left :(");
            };
        }

        public override void LoadContent() {
            btn?.LoadContent();
        }

        public override void FixedUpdate(float? delta) {
            btn.FixedUpdate(delta);
        }

        public override void GameDraw(SpriteBatch spriteBatch) {
        }

        public override void UiDraw(UiSpriteBatch spriteBatch) {
            btn.UiDraw(spriteBatch);
        }

        public override void Dispose() {
            base.Dispose();
        }
    }
}
