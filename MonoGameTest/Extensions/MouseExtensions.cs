//
//  Mouse.cs
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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Extensions {

    public static class MouseExtensions {

        /// <summary>if Mouse is within given bounds</summary>
        /// <returns>True if in bounds</returns>
        /// <param name="mouse">MouseState</param>
        /// <param name="gfxDevice">Graphics Device</param>
        public static bool In(this MouseState mouse, Vector2 region, Vector2 size) {
            var Region = new Rectangle(region.ToPoint(), size.ToPoint());
            return Region.Contains(mouse.X, mouse.Y);
        }

        /// <summary>if Mouse is within Viewport's Bounds</summary>
        /// <returns>True if in bounds</returns>
        /// <param name="mouse">MouseState</param>
        /// <param name="gfxDevice">Graphics Device</param>
        public static bool In(this MouseState mouse, GraphicsDevice gfxDevice) {
            return gfxDevice.Viewport.Bounds.Contains(new Vector2(mouse.X, mouse.Y));
        }

        public static Vector2 ToVector2(this MouseState mouse) {
            return new Vector2(mouse.X, mouse.Y);
        }
    }
}