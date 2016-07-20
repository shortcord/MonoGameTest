using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShortCord.MonoGame.Graphics;

namespace ShortCord.MonoGame {
    /// <summary>
    /// Various functions
    /// </summary>
    public static class Utilities {

        /// <summary>
        /// Current <see cref="GraphicsDevice"/> if any
        /// </summary>
        internal static GraphicsDevice graphicsDevice {
            get {
                if (_gfxDevice == null) {
                    _gfxDevice = ServiceManager.GetService<GraphicsDeviceManager>().GraphicsDevice;
                }
                return _gfxDevice;
            }

        }
        static GraphicsDevice _gfxDevice;

        /// <summary>
        /// Create a Border using <see cref="CreateBasicTexture(Point, Color?)"/>
        /// </summary>
        /// <param name="size">Size of texture using <see cref="Point"/></param>
        /// <param name="color"><see cref="Color"/> of Texture</param>
        /// <returns>Bordered <see cref="Texture2D"/></returns>
        public static Texture2D CreateBorderedTexture(Point? size = null, Color? color = null) {
            Texture2D toReturn = new Texture2D(graphicsDevice, size ?? new Point(50));
            CreateBorder(ref toReturn, 1, color ?? Color.Red);
            return toReturn;
        }

        /// <summary>
        /// Create a basic <see cref="Texture2D"/>.<para/>
        /// This method shouldn't be called more than needed as it may reduce performance.<para/>
        /// Calling this method implys <see cref="GraphicsDeviceManager"/> isn't null.
        /// </summary>
        /// <param name="width">Width of the Texture</param>
        /// <param name="height">Height of the Texture</param>
        /// <param name="color"><see cref="Color"/> of the Texture. If null defaults to <see cref="Color.Red"/></param>
        public static Texture2D CreateBasicTexture(int width = 1, int height = 1, Color? color = null) {
            Texture2D toReturn = new Texture2D(graphicsDevice, width, height);
            FillTexture(ref toReturn, (color ?? Color.Red));
            return toReturn;
        }
        public static Texture2D CreateBasicTexture(Point size, Color? color = null) => CreateBasicTexture(size.X, size.Y, color);

        /// <summary>
        /// Creates a crosshair style sprite
        /// </summary>
        /// <param name="size">Size of crosshair, 1:1 ratio</param>
        /// <param name="color">Color of crosshair; Defaults to <see cref="Color.Red"/> if null</param>
        public static SpriteDefinition CreateCrossTexture(int size = 50, Color? color = null) {
            Texture2D crossTexture;
            SpriteDefinition toReturn;

            using (var autoTarget = new RenderTargetAutomation(new Point(size))) {
                var gfxDevice = graphicsDevice;
                //The way im doing this makes it so there is always one overlaping regardless of size
                using (var texture = CreateBasicTexture(1, 1, color)) {
                    autoTarget.Begin();

                    for (int i = 0; i < size; i++) {
                        autoTarget.Draw(texture,
                            position: new Vector2(size / 2, i),
                            color: Color.White);
                    }

                    for (int i = 0; i < size; i++) {
                        autoTarget.Draw(texture,
                            position: new Vector2(i, size / 2),
                            color: Color.White);
                    }
                }
                
                gfxDevice = null;

                crossTexture = autoTarget.End();
            }

            toReturn = new SpriteDefinition(crossTexture);

            return toReturn;
        }

        /// <summary>
        /// Fill a <see cref="Texture2D"/> with a single color.
        /// </summary>
        /// <param name="texture"><see cref="Texture2D"/> to fill</param>
        /// <param name="color"><see cref="Color"/> wanted</param>
        public static void FillTexture(ref Texture2D texture, Color color) {
            Color[] colorData = new Color[texture.Width * texture.Height];
            for (uint i = 0; i < colorData.Length; i++) {
                colorData[i] = color;
            }
            texture.SetData(colorData);
        }

        /// <summary>
        /// Create a <see cref="Texture2D"/> border<para/>
        /// For easier use see <see cref="CreateBorderedTexture(Point?, Color?)"/>
        /// </summary>
        /// <param name="texture"><see cref="Texture2D"/> to use</param>
        /// <param name="borderWidth">Width of border</param>
        /// <param name="color"><see cref="Color"/> of border</param>
        public static void CreateBorder(ref Texture2D texture, int borderWidth, Color color) {
            //http://stackoverflow.com/a/13894276/4366411
            Color[] colors = new Color[texture.Width * texture.Height];

            for (int x = 0; x < texture.Width; x++) {
                for (int y = 0; y < texture.Height; y++) {
                    bool colored = false;
                    for (int i = 0; i <= borderWidth; i++) {
                        if (x == i || y == i || x == texture.Width - 1 - i || y == texture.Height - 1 - i) {
                            colors[x + y * texture.Width] = color;
                            colored = true;
                            break;
                        }
                    }

                    if (colored == false)
                        colors[x + y * texture.Width] = Color.Transparent;
                }
            }
            texture.SetData(colors);
        }
    }
}
