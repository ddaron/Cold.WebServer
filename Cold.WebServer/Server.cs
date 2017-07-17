using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cold.WebServer
{
    public class Server
    {
        public string ContentPath { get; set; }
        
        private IPAddress _listenOn { get; }
        private int _port { get; }

        private readonly CancellationTokenSource _cts;
        private readonly RequestFactory _requestFactory;
        private readonly ClientHandler _clientHandler;
        private readonly ResponseFactory _responseFactory;

        public Server(IPAddress listenOn, int port) : this()
        {
            _listenOn = listenOn;
            _port = port;
        }

        public Server(string listenOn, int port) : this()
        {
            _listenOn = IPAddress.Parse(listenOn);
            _port = port;
        }

        private Server()
        {
            _cts = new CancellationTokenSource();
            _requestFactory = new RequestFactory();
            _responseFactory = new ResponseFactory();
            _clientHandler = new ClientHandler(_requestFactory, _responseFactory);
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
                var client = await listener.AcceptTcpClientAsync();
                
                _clientHandler.HandleConnection(client, ct);
            }
        }
    }
}