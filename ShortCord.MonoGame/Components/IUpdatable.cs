using Microsoft.Xna.Framework;

namespace ShortCord.MonoGame.Components {
    public interface IUpdatable {
        bool UpdateEnabled { get; }
        bool FixedUpdateEnabled { get; }

        void Update(float delta);
        void FixedUpdate(float? delta);
    }
}
