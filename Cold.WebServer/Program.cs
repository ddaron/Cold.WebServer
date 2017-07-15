using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cold.WebServer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
           var server = new Server("127.0.0.1", 11000);
            server.Run();
        }
    }
}