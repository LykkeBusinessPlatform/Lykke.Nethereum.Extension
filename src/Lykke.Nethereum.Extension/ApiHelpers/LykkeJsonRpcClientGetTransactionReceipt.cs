using System;
using System.Threading.Tasks;
using Lykke.Nethereum.Extension.Exceptions;
using Nethereum.JsonRpc.Client.RpcMessages;
using Nethereum.RPC.Eth.DTOs;
using Newtonsoft.Json;

namespace Lykke.Nethereum.Extension.ApiHelpers
{
    public static class LykkeJsonRpcClientGetTransactionReceipt
    {
        /// <summary>
        /// Returns the receipt of a transaction by transaction hash.
        /// https://github.com/ethereum/wiki/wiki/JSON-RPC#eth_gettransactionreceipt
        /// </summary>
        /// <returns>A transaction receipt object, or null when no receipt was found</returns>
        public static async Task<TransactionReceipt> GetTransactionReceiptAsync(this ILykkeJsonRpcClient client, string transactionHash)
        {
            var result = await client.ExecuteRpcBatchAsync(
                new RpcRequestMessage(1, ApiMethodNames.eth_getTransactionReceipt, transactionHash));

            if (result.Count != 1)
            {
                throw new Exception($"Unexpected result from RPC: {JsonConvert.SerializeObject(result)}");
            }

            if (result[0].Error != null)
            {
                throw new JsonRpcClientRpcException(result[0].Error.Message, result[0].Error.Code);
            }

            var receipt = JsonConvert.DeserializeObject<TransactionReceipt>(result[0].Result.ToString());
            return receipt;
        }
    }
}
