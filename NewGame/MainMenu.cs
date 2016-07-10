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

            btn = new Button(
                "Example Button",
                fitToText: false,
                size: new Microsoft.Xna.Framework.Point(180, 21),
                position: new Microsoft.Xna.Framework.Point(200, 20)
                );

            Objects.Add(btn);

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

        public override void Dispose() {
            base.Dispose();
        }
    }
}
