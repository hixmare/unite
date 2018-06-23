using System;

namespace ClientRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            new Game(targetElapsedTime: TimeSpan.FromSeconds(1f / 60f)).Run();
        }
    }
}
