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
    }
}
