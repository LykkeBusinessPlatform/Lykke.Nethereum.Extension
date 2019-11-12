using System;

namespace Lykke.Nethereum.Extension.Exceptions
{
    public class JsonRpcClientRpcException : Exception
    {
        public int Code { get; set; }

        public JsonRpcClientRpcException(string message, int code) : base(message)
        {
            Code = code;
        }
    }
}
