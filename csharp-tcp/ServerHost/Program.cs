using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace serverclient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new TcpServer().StartAsync();
        }
    }
}
