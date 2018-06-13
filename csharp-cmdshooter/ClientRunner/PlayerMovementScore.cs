using System;

namespace ClientRunner
{
    public class PlayerMovementScore : IGameComponent
    {
        private (Int32 x, Int32 y) PreviousPosition { get; set; }

        public Batch Batch { get; }
        public Player Player { get; }
        public Int32 Score { get; private set; }


        public PlayerMovementScore(Batch batch, Player player)
        {
            Batch = batch;
            Player = player;
        }

        public void Draw() => Batch.Draw($"Score: {Score}", 0, Batch.Height - 2);

        public void Update()
        {
            Score += Math.Abs(PreviousPosition.x - Player.X);
            Score += Math.Abs(PreviousPosition.y - Player.Y);

            PreviousPosition = (Player.X, Player.Y);
        }
    }
}