using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace serverclient
{
    public class TcpServer
    {
        private TcpListener Listener { get; }
        public Boolean KeepAlive { get; set; } = true;

        public TcpServer()
        {
            Listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 13000);
        }

        public async Task StartAsync()
        {
            Listener.Start();
            Console.WriteLine("Server started");
            ICollection<Task> processingTasks = new List<Task>();

            try
            {
                while (KeepAlive)
                {
                    Console.WriteLine("Waiting for client to connect");
                    var client = await Listener.AcceptTcpClientAsync();
                    processingTasks.Add(ProcessClient(client));
                }
            }
            finally
            {
                await Task.WhenAll(processingTasks);
                Console.WriteLine("All pending connections have been closed, closing server in 5 seconds");
                await Task.Delay(5000);
            }
        }

        private async Task ProcessClient(TcpClient client)
        {
            var clientIp = client.Client.RemoteEndPoint;
            try
            {
                using (var netStream = client.GetStream())
                {
                    Console.WriteLine($"Established connection with client {clientIp}");

                    var msg = String.Empty;
                    var reader = new StreamReader(netStream);
                    while (true)
                    {
                        msg = await reader.ReadLineAsync();
                        if (msg.Equals(null)) { continue; }

                        Console.WriteLine($"Message from {clientIp} >> {msg}");

                        if (msg.Equals("--quit")) { break; }
                        if (msg.Equals("--stop"))
                        {
                            KeepAlive = false;
                            Listener.Stop();
                            break;
                        }
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally
            {
                client.Close();
                Console.WriteLine($"Finished connection with client {clientIp}");
            }
        }
    }
}