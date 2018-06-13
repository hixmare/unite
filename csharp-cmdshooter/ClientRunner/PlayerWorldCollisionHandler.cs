using System;

namespace ClientRunner
{
    public class PlayerWorldCollisionHandler : IGameComponent
    {
        private Player Player { get; }
        private World World { get; }

        public PlayerWorldCollisionHandler(Player player, World world)
        {
            Player = player;
            World = world;
        }

        public void Draw() { }

        public void Update()
        {
            int x = 0, y = 0;

            if (Player.X < 0) { x = -Player.X; }
            else if (Player.X + Player.Width > World.Width) { x = -((Player.X + Player.Width) - World.Width); }

            if (Player.Y < 0) { y = -Player.Y; }
            else if (Player.Y + Player.Height > World.Height) { y = -((Player.Y + Player.Height) - World.Height); }

            Player.Move(x, y);
        }
    }
}