using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Lykke.Nethereum.Extension.Exceptions;
using Lykke.Nethereum.Extension.Tools;
using Nethereum.JsonRpc.Client.RpcMessages;
using Newtonsoft.Json;

namespace Lykke.Nethereum.Extension
{
    public class LykkeJsonRpcClient: ILykkeJsonRpcClient
    {
        private readonly string _httpsConnectionString;
        private readonly IHttpClientFactory _httpFactory;

        public LykkeJsonRpcClient(string httpsConnectionString)
        {
            _httpsConnectionString = httpsConnectionString;
            _httpFactory = GeneratorHttpClientFactory.BuildHttpClientFactory();
        }

        public async Task<List<RpcResponseMessage>> ExecuteRpcBatchAsync(params RpcRequestMessage[] messages)
        {
            using (var client = _httpFactory.CreateClient())
            {
                var result = await client.PostAsync(_httpsConnectionString, new JsonContent(messages));
                var content = result.Content != null 
                    ? await result.Content.ReadAsStringAsync() 
                    : string.Empty;

                if (result.StatusCode != HttpStatusCode.OK)
                    throw new JsonRpcClientTransportException(result.StatusCode, content);

                var response = JsonConvert.DeserializeObject<List<RpcResponseMessage>>(content);

                return response;
            }
        }
    }
}
