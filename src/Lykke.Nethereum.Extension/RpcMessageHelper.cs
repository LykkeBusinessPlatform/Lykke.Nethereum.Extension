using System;
using Nethereum.JsonRpc.Client.RpcMessages;

namespace Lykke.Nethereum.Extension
{
    public static class RpcResponseMessageHelper
    {
        public static int? IdAsInteger(this RpcResponseMessage message)
        {
            if (message.Id == null)
                return null;

            if (int.TryParse(message.Id.ToString(), out var id))
            {
                return id;
            }

            return null;
        }

        public static string IdAsString(this RpcResponseMessage message)
        {
            var id = message.Id.ToString();
            return id;
        }

        /// <summary>
        /// Try convert ID from message to GUID.
        /// if id null or cannot cast to guid then return Guid.Empty
        /// </summary>
        /// <returns>Id as GUID or Guid.Empty</returns>
        public static Guid IdAsGuid(this RpcResponseMessage message)
        {
            if (message.Id == null)
                return Guid.Empty;

            if (Guid.TryParse(message.Id.ToString(), out var id))
            {
                return id;
            }

            return Guid.Empty;
        }

        public static string DataAsString(this RpcResponseMessage message)
        {
            return message.Result?.ToString();
        }
    }
}
