using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Nethereum.Extension.Exceptions;
using Nethereum.JsonRpc.Client.RpcMessages;
using Newtonsoft.Json;

namespace Lykke.Nethereum.Extension.ApiHelpers
{
    public static class LykkeJsonRpcClientGetBalance
    {
        /// <summary>
        /// Get Ether balance by address
        /// </summary>
        /// <returns>Balance in wei</returns>
        public static async Task<int> GetEtherBalanceAsync(this ILykkeJsonRpcClient client, string address)
        {
            var result = await client.ExecuteRpcBatchAsync(
                new RpcRequestMessage(1,  ApiMethodNames.eth_getBalance, address, "latest"));

            if (result.Count != 1)
            {
                throw new Exception($"Unexpected result from RPC: {JsonConvert.SerializeObject(result)}");
            }

            if (result[0].Error != null)
            {
                throw new JsonRpcClientRpcException(result[0].Error.Message, result[0].Error.Code);
            }

            var data = result[0].DataAsString();
            var balance = Convert.ToInt32(data, 16);
            return balance;
        }
    }
}
