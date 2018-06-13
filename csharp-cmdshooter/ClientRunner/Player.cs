using System;

namespace ClientRunner
{
    public class Player : IGameComponent
    {
        public Char[,] DefaultTexture { get; } = new[,]{
            {'/', '@', '\\'},
            {'@', '@', '@'},
            {'\\', '@', '/'}
        };


        public ConsoleColor[,] ColorMap { get; } = new[,]{
            {ConsoleColor.Yellow, ConsoleColor.Magenta, ConsoleColor.Yellow},
            {ConsoleColor.Magenta, ConsoleColor.Green, ConsoleColor.Magenta},
            {ConsoleColor.Yellow, ConsoleColor.Magenta, ConsoleColor.Yellow}
        };

        public Batch Batch { get; }
        public ConsoleColor Color { get; set; }
        public Int32 X { get; private set; }
        public Int32 Y { get; private set; }
        public Int32 Width { get; private set; }
        public Int32 Height { get; private set; }

        public Player(Batch batch, Int32 x = 0, Int32 y = 0)
        {
            Batch = batch;
            X = x;
            Y = y;
            Width = DefaultTexture.GetLength(0);
            Height = DefaultTexture.GetLength(1);
        }

        public void Move(Int32 x, Int32 y) { X += x; Y += y; }

        public void Draw() => Batch.Draw(DefaultTexture, X, Y, ColorMap);

        public void Update() { }
    }
}