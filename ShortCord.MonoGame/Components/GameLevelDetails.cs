using System;

namespace ShortCord.MonoGame.Components {
    public class GameLevelDetails : EventArgs {

        public GameLevelDetails(int id, string name) {
            if (id < 0) { throw new ArgumentOutOfRangeException($"{nameof(ID)} must not be negative"); }
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentNullException($"{nameof(name)} must not be null or empty"); }

            ID = id;
            FriendlyName = name;
        }

        public int ID { get; set; } //private to i can set the .Empty to a negative number without throwing
        public string FriendlyName { get; set; }
    }
}
