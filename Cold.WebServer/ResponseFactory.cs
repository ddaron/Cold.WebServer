using System.Collections.Generic;

namespace Cold.WebServer
{
    public class ResponseFactory
    {
        public IHttpResponse Create(IHttpRequest request)
        {
            return new OkHttpResponse("<h1>dupa</h1>", new List<string>()
            {
                "Content-Type: text/html"
            });
        }
    }
}