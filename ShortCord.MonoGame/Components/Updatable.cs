namespace ShortCord.MonoGame.Components {
    public abstract class Updatable : IUpdatable, IComponent {
        public bool FixedUpdateEnabled { get; protected set; } = false;
        public bool UpdateEnabled { get; protected set; } = false;

        public virtual void Update(float delta) { }
        public virtual void FixedUpdate(float? delta) {}
    }
}
