using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cold.WebServer
{
    public sealed class ClientHandler
    {
        private readonly TimeSpan _timeout = TimeSpan.FromSeconds(15);
        private readonly RequestFactory _requestFactory;
        private readonly ResponseFactory _responseFactory;

        public ClientHandler(RequestFactory requestFactory, ResponseFactory responseFactory)
        {
            _requestFactory = requestFactory;
            _responseFactory = responseFactory;
        }

        public async void HandleConnection(TcpClient client, CancellationToken ct)
        {
            using (client)
            {
                var buf = new byte[1024*4];
                var stream = client.GetStream();
                while (!ct.IsCancellationRequested)
                {
                    //TASK: give user timeout if they dont finish the request
                    var timeoutTask = Task.Delay(_timeout);

                    //TASK: read the stream
                    var amountReadTask = stream.ReadAsync(buf, 0, buf.Length, ct);

                    //TASK: combine above tasks for race condition
                   var completedTask = await Task.WhenAny(timeoutTask, amountReadTask)
                       .ConfigureAwait(false);

                    //If first awaited task is timeout, write to stream that the client
                    //timed out, and break;
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
                    var request = _requestFactory.Create(buf);
                    var response = _responseFactory.Create(request);
                    var bytes = response.ToHttpByteArray();

                    await stream.WriteAsync(bytes, 0, bytes.Length);
                    break;
//                        .ConfigureAwait(false);
                }
            }

            Console.WriteLine("Client ({0}) disconnected");
        }
    }
}