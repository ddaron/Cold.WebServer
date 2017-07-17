using System;
using System.Collections.Generic;
using System.Text;

namespace Cold.WebServer
{
    public class OkHttpResponse : BaseHttpResponse
    {
        public OkHttpResponse(string content, IEnumerable<string> httpHeaders) : base(HttpStatusCode.Ok, WebServer.HttpVersion.HTTP11, "OK", content, httpHeaders)
        {
        }
    }
}