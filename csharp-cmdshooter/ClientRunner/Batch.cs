using System;
using System.Text;

namespace ClientRunner
{
    public class Batch
    {
        private ConsoleColor PreviousColor;
        private (Char[,] screen, Char[,] buffer) Tiles { get; set; }
        private ConsoleColor[,] ColorBuffer { get; }

        public Byte Width { get; }
        public Byte Height { get; }
        public Char ClearCharacter { get; set; }

        public Batch(Byte width, Byte height, Char clear = '.')
        {
            Width = width;
            Height = height;
            ClearCharacter = clear;

            Tiles = (new Char[Width, Height], new Char[Width, Height]);
            ColorBuffer = new ConsoleColor[Width, Height];
            ClearColorBuffer();
        }

        public void ClearColorBuffer()
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++) { ColorBuffer[x, y] = ConsoleColor.White; }
            }
        }

        public void ClearBuffer()
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++) { this[x, y] = ClearCharacter; }
            }
        }

        public StringBuilder TransposeScreen()
        {
            var result = new StringBuilder();
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++) { result.Append(Tiles.screen[x, y]); }
                result.Append(Environment.NewLine);
            }
            return result;
        }

        public void Draw()
        {
            Swap();

            Console.SetCursorPosition(0, 0);
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    /* Change color only if it has to, as changing the foreground is costly */
                    var color = ColorBuffer[x, y];
                    if (!color.Equals(PreviousColor))
                    {
                        Console.ForegroundColor = color;
                        PreviousColor = color;
                    }
                    Console.Write(Tiles.screen[x, y]);
                }
                Console.Write(Environment.NewLine);
            }
            ClearBuffer();
            ClearColorBuffer();
        }

        public void Draw(Char character, Int32 x, Int32 y) => this[x, y] = character;

        public void Draw(String text, Int32 x, Int32 y)
        {
            for (var i = x; i < x + text.Length; i++)
            {
                this[i, y] = text.ToCharArray()[i - x];
            }
        }

        public void Draw(Char[,] texture, Int32 x = 0, Int32 y = 0, ConsoleColor[,] colorMap = null)
        {
            var width = texture.GetLength(0);
            var height = texture.GetLength(1);
            for (Int32 i = x, i1 = 0; i < x + width; i++, i1++)
            {
                for (Int32 j = y, j1 = 0; j < y + texture.GetLength(1); j++, j1++)
                {
                    this[i, j] = texture[i1, j1];
                    if (colorMap != null) { SetColor(colorMap[i1, j1], i, j); }
                }
            }
        }

        private void Swap() => Tiles = (Tiles.buffer, Tiles.screen);

        Char this[Int32 x, Int32 y]
        {
            get
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height) { throw new IndexOutOfRangeException(); }
                return Tiles.buffer[x, y];
            }

            set
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height) { return; }
                Tiles.buffer[x, y] = value;
            }
        }

        ConsoleColor GetColor(Int32 x, Int32 y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height) { throw new IndexOutOfRangeException(); }
            return ColorBuffer[x, y];
        }

        void SetColor(ConsoleColor value, Int32 x, Int32 y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height) { return; }
            ColorBuffer[x, y] = value;
        }
    }
}