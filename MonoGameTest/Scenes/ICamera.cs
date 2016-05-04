//
//  ICamera.cs
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
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scenes {

    public interface ICamera {
        Vector2 Position { get; }
        float Zoom { get; }
        float Rotation { get; }
        Matrix TransformationMatrix { get; }
        Rectangle ViewportWorldBoundry { get; }
        IEntity ControllingEntity { get; }

        //methods
        Vector2 ScreenToWorld(Vector2 screenPosition);

        Vector2 WorldToScreen(Vector2 worldPosition);

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}