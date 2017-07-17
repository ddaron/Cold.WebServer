namespace Cold.WebServer
{
    public class RequestHttpGet : RequestBase
    {
        public string FilePath { get; }

        public RequestHttpGet(string filePath)
        {
            FilePath = filePath;
        }
    }
}