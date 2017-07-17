using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cold.WebServer
{
    public class RequestFactory
    {
        private readonly IEnumerable<string> supportedVerbs;
        private readonly IEnumerable<string> supportedHttpVersions;

        public RequestFactory()
        {
            supportedVerbs = new List<string>()
            {
                HttpVerb.Get
            };

            supportedHttpVersions = new List<string>()
            {
                HttpVersion.HTTP1,
                HttpVersion.HTTP11
            };

        }

        public IHttpRequest Create(byte[] buffer)
        {
            var message = Encoding.ASCII.GetString(buffer);
            var messageLines = message.Split(new string[] { "\r\n", "\n"}, StringSplitOptions.None);
            var httpHeader = messageLines[0].Split(' ');
            var verb = httpHeader[0];
            var filePath = httpHeader[1];
            var httpVersion = httpHeader[2];

            if (!supportedVerbs.Contains(verb) || !supportedHttpVersions.Contains(httpVersion)) return RequestBase.NULL;
            
            switch (verb)
            {
                case "GET":
                    return new RequestHttpGet(buffer);
                default:
                    return RequestBase.NULL;
            }

        }
    }
}