using System;

namespace ShortCord.MonoGame.Extensions {
    public static class FloatExtenstions {
        public static float ToFloat(this double value) {
            return Convert.ToSingle(value);
        }
    }
}
