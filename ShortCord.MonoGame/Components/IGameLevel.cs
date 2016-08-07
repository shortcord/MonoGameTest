namespace ShortCord.MonoGame.Components {
    public interface IGameLevel {
        GameLevelDetails Details { get; }
        bool Debug { get; }
        void Start();
        void LoadContent();
        void UnloadContent();
    }
}
