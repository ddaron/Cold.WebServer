using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace Cold.WebServer
{
    public interface IHttpResponse
    {
        HttpStatusCode StatusCode { get; }
        string HttpVersion { get; }
        string ResponseMessage { get; }
        IEnumerable<string> HttpHeaders { get; }
        string Content { get; }

        byte[] ToHttpByteArray();
    }
}