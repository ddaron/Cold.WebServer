using System.Collections.Generic;

namespace Cold.WebServer
{
    public interface IHttpRequest
    {
        string HttpMethod { get; }
        string RequestPath { get; }
        string HttpVersion { get; }
        IEnumerable<string> HttpHeaders { get; }
        string MessageBody { get; }
    }
}