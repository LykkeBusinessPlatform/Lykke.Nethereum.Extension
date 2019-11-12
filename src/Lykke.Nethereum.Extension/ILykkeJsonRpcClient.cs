using System.Collections.Generic;
using System.Threading.Tasks;
using Nethereum.JsonRpc.Client.RpcMessages;

namespace Lykke.Nethereum.Extension
{
    public interface ILykkeJsonRpcClient
    {
        /// <summary>
        /// Execute one or many requests.
        /// The batch can include a request with different eth methods
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        Task<List<RpcResponseMessage>> ExecuteRpcBatchAsync(params RpcRequestMessage[] messages);
    }
}
