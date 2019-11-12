using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Nethereum.Extension.Exceptions;
using Nethereum.JsonRpc.Client.RpcMessages;
using Newtonsoft.Json;

namespace Lykke.Nethereum.Extension.ApiHelpers
{
    public static class LykkeJsonRpcClientGetBlockNumber
    {
        /// <summary>
        /// Get number of last block
        /// </summary>
        /// <returns>Number of last block</returns>
        public static async Task<Int64> GetBlockNumberAsync(this ILykkeJsonRpcClient client)
        {
            var result = await client.ExecuteRpcBatchAsync(
                new RpcRequestMessage(1,  ApiMethodNames.eth_blockNumber));

            if (result.Count != 1)
            {
                throw new Exception($"Unexpected result from RPC: {JsonConvert.SerializeObject(result)}");
            }

            if (result[0].Error != null)
            {
                throw new JsonRpcClientRpcException(result[0].Error.Message, result[0].Error.Code);
            }

            var data = result[0].DataAsString();
            var balance = Convert.ToInt64(data, 16);
            return balance;
        }
    }

}
