namespace ShortCord.MonoGame.Components {
    public interface IGameLevel {
        GameLevelDetails Details { get; }
        void Start();
        void LoadContent();
        void UnloadContent();
    }
}
