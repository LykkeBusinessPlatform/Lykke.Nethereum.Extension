using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Lykke.Nethereum.Extension.Exceptions;
using Lykke.Nethereum.Extension.Tools;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.JsonRpc.Client;
using Nethereum.JsonRpc.Client.RpcMessages;
using Newtonsoft.Json;

namespace Lykke.Nethereum.Extension
{
    public class LykkeJsonRpcClient: ILykkeJsonRpcClient, IDisposable
    {
        private readonly string _httpsConnectionString;
        private readonly TimeSpan _timeout;
        private readonly IHttpClientFactory _httpFactory;
        private readonly ServiceProvider _serviceProvider;

        public LykkeJsonRpcClient(string httpsConnectionString, TimeSpan timeout)
        {
            _httpsConnectionString = httpsConnectionString;
            _timeout = timeout;
            _serviceProvider = GeneratorHttpClientFactory.BuildServiceProvider();
            _httpFactory = _serviceProvider.GetService<IHttpClientFactory>();
        }

        public LykkeJsonRpcClient(string httpsConnectionString): this(httpsConnectionString, TimeSpan.FromSeconds(10))
        {
        }

        public async Task<List<RpcResponseMessage>> ExecuteRpcBatchAsync(params RpcRequestMessage[] messages)
        {
            try
            {
                using (var client = _httpFactory.CreateClient())
                {
                    client.Timeout = _timeout;
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
            catch (OperationCanceledException e)
            {
                throw new RpcClientTimeoutException($"Rpc call Timeout ({_timeout})", e);
            }
            
        }

        public void Dispose()
        {
            _serviceProvider?.Dispose();
        }
    }
}
