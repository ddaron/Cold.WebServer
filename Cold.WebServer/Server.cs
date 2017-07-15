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
        
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly HttpRequestHandler _httpRequestHandler = new HttpRequestHandler();

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
            var listener = new TcpListener(_listenOn, _port);
            try
            {
                listener.Start();
                AcceptClientsAsync(listener, _cts.Token);
                while (true) continue;
            }
            finally
            {
                _cts.Cancel();
                listener.Stop();
            }
        }

        private async void AcceptClientsAsync(TcpListener listener, CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                TcpClient client = await listener.AcceptTcpClientAsync()
                    .ConfigureAwait(false);
                
                _httpRequestHandler.HandleConnection(client, ct);
            }
        }
    }
}