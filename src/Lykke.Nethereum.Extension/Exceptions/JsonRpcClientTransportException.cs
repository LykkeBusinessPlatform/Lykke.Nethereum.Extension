using System;
using System.Net;

namespace Lykke.Nethereum.Extension.Exceptions
{
    public class JsonRpcClientTransportException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Content { get; set; }

        public JsonRpcClientTransportException(HttpStatusCode statusCode, string content) : base($"Http transport error. Status code: {statusCode}")
        {
            StatusCode = statusCode;
            Content = content;
        }
    }
}
