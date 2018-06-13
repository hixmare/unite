using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (TcpClient client = new TcpClient())
            {
                await client.ConnectAsync(IPAddress.Parse("127.0.0.1"), 13000);
                using (var netStream = client.GetStream())
                {
                    var writer = new StreamWriter(netStream);
                    writer.AutoFlush = true;
                    var msg = String.Empty;
                    do
                    {
                        msg = Console.ReadLine();
                        writer.WriteLine(msg);
                    } while (!msg.Equals("--exit"));
                }
            }
        }
    }
}
