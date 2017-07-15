using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cold.WebServer
{
    public class Server
    {
        private IPAddress _listenOn { get; }
        private int _port { get; }
        
        private readonly TimeSpan _timeout = TimeSpan.FromSeconds(15);
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public Server(IPAddress listenOn, int port)
        {
            _listenOn = listenOn;
            _port = port;
        }

        public Server(string listenOn, int port)
        {
            _listenOn = IPAddress.Parse(listenOn);
            _port = port;
        }

        public void Run()
        {
            TcpListener listener = new TcpListener(_listenOn, _port);
            try
            {
                listener.Start();
                //just fire and forget. We break from the "forgotten" async loops
                //in AcceptClientsAsync using a CancellationToken from `cts`
                AcceptClientsAsync(listener, _cts.Token);
                while (true) continue;
            }
            finally

            {
                _cts.Cancel();
                listener.Stop();
            }
        }

        async Task AcceptClientsAsync(TcpListener listener, CancellationToken ct)
        {
            var clientCounter = 0;
            while (!ct.IsCancellationRequested)
            {
                TcpClient client = await listener.AcceptTcpClientAsync()
                    .ConfigureAwait(false);
                clientCounter++;
//once again, just fire and forget, and use the CancellationToken
//to signal to the "forgotten" async invocation.
                EchoAsync(client, clientCounter, ct);
            }
        }

        async Task EchoAsync(TcpClient client,
            int clientIndex,
            CancellationToken ct)
        {
            Console.WriteLine("New client ({0}) connected", clientIndex);
            using (client)
            {
                var buf = new byte[4096];
                var stream = client.GetStream();
                while (!ct.IsCancellationRequested)
                {
//under some circumstances, it's not possible to detect
//a client disconnecting if there's no data being sent
//so it's a good idea to give them a timeout to ensure that 
//we clean them up.
                    var timeoutTask = Task.Delay(_timeout);
                    var amountReadTask = stream.ReadAsync(buf, 0, buf.Length, ct);
                    var completedTask = await Task.WhenAny(timeoutTask, amountReadTask)
                        .ConfigureAwait(false);
                    if (completedTask == timeoutTask)
                    {
                        var msg = Encoding.ASCII.GetBytes("Client timed out");
                        await stream.WriteAsync(msg, 0, msg.Length);
                        break;
                    }
//now we know that the amountTask is complete so
//we can ask for its Result without blocking
                    var amountRead = amountReadTask.Result;
                    if (amountRead == 0) break; //end of stream.
                    await stream.WriteAsync(buf, 0, amountRead, ct)
                        .ConfigureAwait(false);
                }
            }

            Console.WriteLine("Client ({0}) disconnected", clientIndex);
        }
    }
}