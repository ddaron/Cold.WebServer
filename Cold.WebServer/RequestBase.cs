using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Cold.WebServer
{
    public abstract class RequestBase : IHttpRequest
    {
        public static readonly RequestBase NULL = new NullRequest(string.Empty, string.Empty, Enumerable.Empty<string>(), string.Empty, string.Empty);

        protected RequestBase(string httpMethod, string httpVersion, IEnumerable<string> httpHeaders, string messageBody, string requestPath)
        {
            HttpMethod = httpMethod;
            HttpVersion = httpVersion;
            HttpHeaders = httpHeaders;
            MessageBody = messageBody;
            RequestPath = requestPath;
        }

        protected RequestBase(byte[] data)
        {
            var message = Encoding.ASCII.GetString(data);
            
            var lines = message.Split(new string[] {"\r\n", "\n"}, StringSplitOptions.None);
            var header = lines[0].Split(' ');
            
            HttpMethod = header[0];
            RequestPath = header[1];
            HttpVersion = header[2];

            var headers = new List<string>();
            lines.Skip(1).TakeWhile(x => !string.IsNullOrEmpty(x)).ToList().ForEach(x => headers.Add(x));

        }

        public string HttpMethod { get; }
        public string RequestPath { get; }
        public string HttpVersion { get; }
        public IEnumerable<string> HttpHeaders { get; }
        public string MessageBody { get; }
    }
    
    public class NullRequest : RequestBase
    {
        public NullRequest(string httpMethod, string httpVersion, IEnumerable<string> httpHeaders, string messageBody, string requestPath) : base(httpMethod, httpVersion, httpHeaders, messageBody, requestPath)
        {
        }

        public NullRequest(byte[] data) : base(data)
        {
        }
    }
}