using System;
using System.IO;

namespace ClientRunner
{
    public static class BatchExtensions
    {
        public static void Print(this Batch self, StreamWriter output)
        {
            if (output.Equals(null)) { throw new ArgumentNullException(nameof(output)); }

            var str = self.TransposeScreen().ToString();
            output.Write(str);
        }
    }
}