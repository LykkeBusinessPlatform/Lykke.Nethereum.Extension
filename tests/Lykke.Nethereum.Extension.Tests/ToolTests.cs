using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Lykke.Nethereum.Extension.Tools;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.JsonRpc.Client.RpcMessages;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Lykke.Nethereum.Extension.Tests
{
    public class ToolTests
    { 
        public static string NodeUrl = "https://ropsten-rpc.linkpool.io/";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GenerateHttpClient()
        {
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            using (var client = httpClientFactory.CreateClient())
            {
                var res = await client.GetStringAsync("https://google.com");
                Console.WriteLine(res);
            }
        }

        [Test]
        public async Task ExecutePostMethod()
        {
            var factory = GeneratorHttpClientFactory.BuildHttpClientFactory();
            var client = factory.CreateClient();

            var res = await client.PostAsync(NodeUrl,
                new JsonContent(new RpcRequestMessage("123aaa", "eth_sendRawTransaction",
                    new[]
                    {
                        "0xf90107828a3c80830f424094e6aa38527aad348ac8db00be258a38af233d090c80b8a49bd9bbc6000000000000000000000000bd58b6dc5c8865d10a7e4cbd81c861905331e8f00000000000000000000000000000000000000000000000008ac7230489e800000000000000000000000000000000000000000000000000000000000000000060000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001ca060c5eb68c34491af244c1b78a0fe271c86b5f1c87dc82d80116dd5f96b2534d9a0298024deae5e6f4e4a3af0dd9f3d9cb20c37f1a83bc4482cb0ca9aa8ed951017"
                    })));

            Console.WriteLine($"Status code: {res.StatusCode}");
            Console.WriteLine($"Status code: {res.Content.ReadAsStringAsync().Result}");

        }

        [Test]
        public async Task Get_Id_RpcResponseMessage_int()
        {
            var factory = GeneratorHttpClientFactory.BuildHttpClientFactory();
            var client = factory.CreateClient();

            var res = await client.PostAsync(NodeUrl,
                new JsonContent(new RpcRequestMessage(143, "eth_sendRawTransaction",
                    new[]
                    {
                        "0xf90107828a3c80830f424094e6aa38527aad348ac8db00be258a38af233d090c80b8a49bd9bbc6000000000000000000000000bd58b6dc5c8865d10a7e4cbd81c861905331e8f00000000000000000000000000000000000000000000000008ac7230489e800000000000000000000000000000000000000000000000000000000000000000060000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001ca060c5eb68c34491af244c1b78a0fe271c86b5f1c87dc82d80116dd5f96b2534d9a0298024deae5e6f4e4a3af0dd9f3d9cb20c37f1a83bc4482cb0ca9aa8ed951017"
                    })));

            Console.WriteLine($"Status code: {res.StatusCode}");

            if (res.StatusCode == HttpStatusCode.OK)
            {
                var responceContent = await res.Content.ReadAsStringAsync();
                Console.WriteLine(responceContent);
                var responce = JsonConvert.DeserializeObject<RpcResponseMessage>(responceContent);
                var id = responce.IdAsInteger();
                Console.WriteLine($"Id: {id}");
                Assert.AreEqual(143, id);
            }
        }

        [Test]
        public async Task Get_Id_RpcResponseMessage_string()
        {
            var factory = GeneratorHttpClientFactory.BuildHttpClientFactory();
            var client = factory.CreateClient();

            var res = await client.PostAsync(NodeUrl,
                new JsonContent(new RpcRequestMessage("qwe 11", "eth_sendRawTransaction",
                    "0xf90107828a3c80830f424094e6aa38527aad348ac8db00be258a38af233d090c80b8a49bd9bbc6000000000000000000000000bd58b6dc5c8865d10a7e4cbd81c861905331e8f00000000000000000000000000000000000000000000000008ac7230489e800000000000000000000000000000000000000000000000000000000000000000060000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001ca060c5eb68c34491af244c1b78a0fe271c86b5f1c87dc82d80116dd5f96b2534d9a0298024deae5e6f4e4a3af0dd9f3d9cb20c37f1a83bc4482cb0ca9aa8ed951017")));

            Console.WriteLine($"Status code: {res.StatusCode}");

            if (res.StatusCode == HttpStatusCode.OK)
            {
                var responceContent = await res.Content.ReadAsStringAsync();
                Console.WriteLine(responceContent);
                var responce = JsonConvert.DeserializeObject<RpcResponseMessage>(responceContent);
                var id = responce.IdAsString();
                Console.WriteLine($"Id: {id}");
                Assert.AreEqual("qwe 11", id);
            }
        }

        [Test]
        public async Task Timeout()
        {
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromMilliseconds(200);
            try
            {
                var result = await client.GetStringAsync("http://blog.cincura.net/");
            }
            catch (HttpRequestException)
            {
                // handle somehow
                Console.WriteLine("HttpRequestException");
            }
            catch (TimeoutException)
            {
                // handle somehow
                Console.WriteLine("TimeoutException");
            }
            catch (OperationCanceledException e)
            {
                // handle somehow
                Console.WriteLine("OperationCanceledException");
                Console.WriteLine(e);
            }
        }
    }
}
