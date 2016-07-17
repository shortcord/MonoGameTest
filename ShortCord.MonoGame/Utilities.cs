using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShortCord.MonoGame {
    /// <summary>
    /// Various functions
    /// </summary>
    public class Utilities {

        /// <summary>
        /// Fill a Texture2D with a single color
        /// </summary>
        /// <param name="texture">Texture2D to fill</param>
        /// <param name="color">Color wanted</param>
        public static void FillTexture(ref Texture2D texture, Color color) {
            Color[] colorData = new Color[texture.Width * texture.Height];
            for (uint i = 0; i < colorData.Length; i++) {
                colorData[i] = color;
            }
            texture.SetData(colorData);
        }
    }
}
