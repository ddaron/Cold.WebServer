namespace Cold.WebServer
{
    public class RequestBase : IHttpRequest
    {
        public static readonly RequestBase NULL = new RequestBase();
    }
}