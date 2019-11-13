using System;
using System.Threading.Tasks;
using Lykke.Nethereum.Extension.Exceptions;
using Nethereum.JsonRpc.Client.RpcMessages;
using Nethereum.RPC.Eth.DTOs;
using Newtonsoft.Json;

namespace Lykke.Nethereum.Extension.ApiHelpers
{
    public static class LykkeJsonRpcClientGetTransactionByHash
    {
        /// <summary>
        /// Returns the information about a transaction requested by transaction hash.
        /// https://github.com/ethereum/wiki/wiki/JSON-RPC#eth_gettransactionbyhash
        /// </summary>
        /// <returns>A transaction object, or null when no transaction was found</returns>
        public static async Task<Transaction> GetTransactionByHashAsync(this ILykkeJsonRpcClient client, string transactionHash)
        {
            var result = await client.ExecuteRpcBatchAsync(
                new RpcRequestMessage(1, ApiMethodNames.eth_getTransactionByHash, transactionHash));

            if (result.Count != 1)
            {
                throw new Exception($"Unexpected result from RPC: {JsonConvert.SerializeObject(result)}");
            }

            if (result[0].Error != null)
            {
                throw new JsonRpcClientRpcException(result[0].Error.Message, result[0].Error.Code);
            }

            if (result[0].Result == null)
                return null;

            var receipt = JsonConvert.DeserializeObject<Transaction>(result[0].Result.ToString());
            return receipt;
        }
    }
}
