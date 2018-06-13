using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ClientRunner
{
    public class Game : IGameComponent
    {
        private Stopwatch GameTime { get; }

        public Batch Batch { get; }
        public List<IGameComponent> Components { get; }
        public Boolean IsRunning { get; private set; }
        public TimeSpan TargetElapsedTime { get; set; }

        public Game(Byte width = 50, Byte height = 22, TimeSpan? targetElapsedTime = null)
        {
            Batch = new Batch(width, height, ' ');
            Components = new List<IGameComponent>();
            GameTime = new Stopwatch();

            TargetElapsedTime = targetElapsedTime ?? TimeSpan.FromSeconds(1f / 24f);

            Initialize();
        }

        private void Initialize()
        {
            var world = new World(Batch, (Byte)(Batch.Width - 10), (Byte)(Batch.Height - 2));
            Components.Add(world);

            var player = new Player(Batch);
            Components.Add(player);

            Components.Add(new PlayerInputHandler(this, Batch, player));
            Components.Add(new PlayerWorldCollisionHandler(player, world));
            Components.Add(new PlayerMovementScore(Batch, player));
            Components.Add(new InstructionsPrinter(Batch));
        }

        public void Run()
        {
            IsRunning = true;
            GameTime.Start();

            while (IsRunning)
            {
                if (GameTime.Elapsed >= TargetElapsedTime)
                {
                    Update();
                    Draw();

                    GameTime.Restart();
                }
            }
        }

        public void Update()
        {
            foreach (var c in Components) { c.Update(); }
        }

        public void Draw()
        {
            foreach (var c in Components) { c.Draw(); }
            Batch.Draw($"FPS: {1000f / GameTime.ElapsedMilliseconds}", 40, 8);
            Batch.Draw();
        }

        public void Close() => IsRunning = false;
    }
}