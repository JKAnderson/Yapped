namespace Yapped
{
    internal class GameMode
    {
        public enum GameType
        {
            DarkSouls2,
            DarkSouls3,
            Sekiro
        }

        public GameType Game { get; }
        public string Name { get; }
        public string Directory { get; }

        public GameMode(GameType game, string name, string directory)
        {
            Game = game;
            Name = name;
            Directory = directory;
        }

        public static readonly GameMode[] Modes =
        {
            new GameMode(GameType.DarkSouls2, "Dark Souls 2", "DS2"),
            new GameMode(GameType.DarkSouls3, "Dark Souls 3", "DS3"),
            new GameMode(GameType.Sekiro, "Sekiro", "SDT"),
        };
    }
}
