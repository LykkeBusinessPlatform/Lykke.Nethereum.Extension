using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Nethereum.Extension.Tools
{
    public static class GeneratorHttpClientFactory
    {
        public static IHttpClientFactory BuildHttpClientFactory()
        {
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            return httpClientFactory;
        }
    }
}
