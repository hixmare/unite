using System;
using System.IO;

namespace ClientRunner
{
    public class PlayerInputHandler : IGameComponent
    {
        public const Int32 DefaultSpeed = 4;

        public Game Game { get; }
        public Batch Batch { get; }
        public Player Player { get; }

        public PlayerInputHandler(Game game, Batch batch, Player player)
        {
            Game = game;
            Batch = batch;
            Player = player;
        }

        public void Draw() { }

        public void Update()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                if (key.Equals(ConsoleKey.W)) { Player.Move(0, -DefaultSpeed); }
                else if (key.Equals(ConsoleKey.S)) { Player.Move(0, DefaultSpeed); }

                if (key.Equals(ConsoleKey.A)) { Player.Move(-DefaultSpeed, 0); }
                else if (key.Equals(ConsoleKey.D)) { Player.Move(DefaultSpeed, 0); }

                if (key.Equals(ConsoleKey.P))
                {
                    var path = Path.Combine(Environment.CurrentDirectory, "screenshots");
                    if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

                    using (var wr = new StreamWriter(Path.Combine(path, DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".txt")))
                    {
                        Batch.Print(wr);
                    }
                }

                if (key.Equals(ConsoleKey.X)) { Game.Close(); }
            }
        }
    }
}