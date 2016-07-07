//
//  MainMenu.cs
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
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameTest;
using MonoGameTest.Entities;

namespace Scenes {
    public class MainMenu : IScene {
        public Dictionary<int, IEntity> EnemySpawns { get; private set; }
        public int ID { get; private set; }
        public string Name { get; private set; }
        public Vector2 PlayerSpawn { get; private set; }
        public ICamera PlayerCamera { get; private set; }
        
        public MainMenu() {
            ID = 0;
            Name = "MainMenu";
            PlayerSpawn = Vector2.Zero;
        }

        public void Load() {

        }

        public void Unload() {

        }

        public void Update(GameTime gameTime) {

        }

        public void Draw(SpriteBatch spriteBatch) {

        }
    }
}
