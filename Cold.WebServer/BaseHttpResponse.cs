using System;
using System.Collections.Generic;
using System.Text;

namespace Cold.WebServer
{
    public abstract class BaseHttpResponse : IHttpResponse
    {
        public HttpStatusCode StatusCode { get; }
        public string HttpVersion { get; }
        public string ResponseMessage { get; }
        public IEnumerable<string> HttpHeaders { get; }
        public string Content { get;}

        protected BaseHttpResponse(HttpStatusCode statusCode 
            , string httpVersion
            , string responseMessage
            , string content, 
            IEnumerable<string> httpHeaders)
        {
            StatusCode = statusCode;
            HttpVersion = httpVersion;
            ResponseMessage = responseMessage;
            Content = content;
            HttpHeaders = httpHeaders;
        }

        public virtual byte[] ToHttpByteArray()
        {
            var sb = new StringBuilder();
            sb.Append(HttpVersion);
            sb.Append(" ");
            sb.Append(((int)StatusCode).ToString());
            sb.Append(" ");
            sb.Append(ResponseMessage);
            sb.Append(Environment.NewLine);
            sb.Append(string.Join(Environment.NewLine, HttpHeaders));
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append(Content);
            sb.Append(Environment.NewLine);
            return Encoding.ASCII.GetBytes(sb.ToString());
        }
    }
}