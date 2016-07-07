//
//  VectorExtensions.cs
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

namespace Extensions {

    public static class VectorExtensions {

        /// <summary>
        /// Convert Vector2 to Vector3
        /// </summary>
        public static Vector3 ToVector3(this Vector2 vect, float z = 0f) {
            return new Vector3(vect, z);
        }

        /// <summary>
        /// Convert Vector2 to Vector4
        /// </summary>
        public static Vector4 ToVector4(this Vector2 vect, float z = 0f, float w = 0f) {
            return new Vector4(vect, z, w);
        }

        /// <summary>
        /// Convert Vector3 to Vector2, Dropping the Z property
        /// </summary>
        public static Vector2 ToVector2(this Vector3 vect) {
            return new Vector2(vect.X, vect.Y);
        }

        /// <summary>
        /// Convert Vector3 to Vector4
        /// </summary>
        public static Vector4 ToVector4(this Vector3 vect, float w = 0f) {
            return new Vector4(vect, w);
        }

        /// <summary>
        /// Convert Vector4 to Vector3, Dropping the W property
        /// </summary>
        public static Vector3 ToVector3(this Vector4 vect) {
            return new Vector3(vect.X, vect.Y, vect.Z);
        }

        /// <summary>
        /// Convert Vector4 to Vector2, Dropping the W and Z property
        /// </summary>
        public static Vector2 ToVector2(this Vector4 vect) {
            return new Vector2(vect.X, vect.Y);
        }
    }
}