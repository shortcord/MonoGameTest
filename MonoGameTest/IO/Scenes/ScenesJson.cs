//
//  ScenesJson.cs
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
using System.Collections.Generic;
using Entities;
using Microsoft.Xna.Framework;
using Scenes;

namespace IO.Scenes {

    public struct ScenesJson {

        public ScenesJson(IScene scene) {
            Name = scene.Name;
            ID = scene.ID;
            PlayerSpawn = scene.PlayerSpawn;
            EnemySpawns = scene.EnemySpawns;
        }

        public string Name { get; set; }
        public int ID { get; set; }
        public Vector2 PlayerSpawn { get; set; }
        public Dictionary<int, IEntity> EnemySpawns { get; set; }
    }
}