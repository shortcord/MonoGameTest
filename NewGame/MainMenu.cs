using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using NewGame.UI;
using ShortCord.MonoGame;
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
            btn = new Button();
        }

        public override void LoadContent() {
            btn?.LoadContent();
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
