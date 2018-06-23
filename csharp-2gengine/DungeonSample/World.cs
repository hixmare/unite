using System;
using System.Collections.Generic;

namespace ClientRunner
{
    public class World : IGameComponent
    {
        public const Char Light = '░';
        public const Char Medium = '▒';
        public const Char Hard = '▓';
        public const Char Black = '█';

        private static Char[] TileTypes { get; } = new[] { Light, Medium };

        private Char[,] Texture { get; set; }

        public Batch Batch { get; }
        public Byte Width { get; set; }
        public Byte Height { get; set; }
        public List<IGameComponent> Components { get; }

        public World(Batch batch, Byte? width = null, Byte? height = null)
        {
            Components = new List<IGameComponent>();

            Batch = batch;
            Width = width ?? Batch.Width;
            Height = height ?? Batch.Height;
            Texture = new Char[Width, Height];

            var r = new Random();
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++) { Texture[i, j] = TileTypes[r.Next(0, TileTypes.Length)]; }
            }
        }

        public void Draw()
        {
            Batch.Draw(Texture);
            foreach (var c in Components) { c.Draw(); }
        }

        public void Update()
        {
            foreach (var c in Components) { c.Update(); }
        }
    }
}