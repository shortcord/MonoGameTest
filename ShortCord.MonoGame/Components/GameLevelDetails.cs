using System;

namespace ShortCord.MonoGame.Components {
    public struct GameLevelDetails {
        static readonly GameLevelDetails emptyValue = new GameLevelDetails() { ID = -1, FriendlyName = "Empty" };

        public GameLevelDetails(int id, string name) {
            if (id < 0) { throw new ArgumentOutOfRangeException($"{nameof(ID)} must not be negative"); }

            ID = id;
            FriendlyName = name;
        }

        public int ID { get; private set; } //private to i can set the .Empty to a negative number without throwing
        public string FriendlyName { get; private set; }

        public static GameLevelDetails Empty { get { return emptyValue; } }
    }
}
