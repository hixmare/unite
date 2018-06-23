namespace ClientRunner
{
    public class InstructionsPrinter : IGameComponent
    {
        public Batch Batch { get; }

        public InstructionsPrinter(Batch batch)
        {
            Batch = batch;
        }

        public void Draw()
        {
            Batch.Draw("W: UP", Batch.Width - 10, 0);
            Batch.Draw("S: DOWN", Batch.Width - 10, 1);
            Batch.Draw("A: LEFT", Batch.Width - 10, 2);
            Batch.Draw("D: RIGHT", Batch.Width - 10, 3);
            Batch.Draw("P: PRINT", Batch.Width - 10, 5);
            Batch.Draw("X: CLOSE", Batch.Width - 10, 6);
        }

        public void Update()
        {

        }
    }
}