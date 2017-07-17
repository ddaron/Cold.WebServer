using System.Collections.Generic;

namespace Cold.WebServer
{
    public class RequestHttpGet : RequestBase
    {
        public RequestHttpGet(string httpVersion, IEnumerable<string> httpHeaders, string messageBody, string requestPath) : base(HttpVerb.Get, httpVersion, httpHeaders, messageBody, requestPath)
        {
        }

        public RequestHttpGet(byte[] data) : base(data)
        {
        }
    }
}