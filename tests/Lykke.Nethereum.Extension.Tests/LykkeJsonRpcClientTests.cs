using System;
using System.Threading.Tasks;
using Lykke.Nethereum.Extension.ApiHelpers;
using Nethereum.JsonRpc.Client.RpcMessages;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Lykke.Nethereum.Extension.Tests
{
    public class LykkeJsonRpcClientTests
    {
        public static string NodeUrl = "https://ropsten-rpc.linkpool.io/";

        [Test]
        public async Task ExecutePost_sendRawTransaction()
        {
            var client = new LykkeJsonRpcClient(NodeUrl);

            var result = await client.ExecuteRpcBatchAsync(new RpcRequestMessage[]
            {
                new RpcRequestMessage(1, "eth_sendRawTransaction",
                    "0xf90107828a3c80830f424094e6aa38527aad348ac8db00be258a38af233d090c80b8a49bd9bbc6000000000000000000000000bd58b6dc5c8865d10a7e4cbd81c861905331e8f00000000000000000000000000000000000000000000000008ac7230489e800000000000000000000000000000000000000000000000000000000000000000060000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001ca060c5eb68c34491af244c1b78a0fe271c86b5f1c87dc82d80116dd5f96b2534d9a0298024deae5e6f4e4a3af0dd9f3d9cb20c37f1a83bc4482cb0ca9aa8ed951017")
            });

            Assert.AreEqual(1, result.Count, "incorrect result count");
            Assert.AreEqual(1, result[0].IdAsInteger(), "incorrect id");

            Console.WriteLine(result.Count);
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }

        [Test]
        public async Task ExecutePost_getBalance()
        {
            var client = new LykkeJsonRpcClient(NodeUrl);

            var result = await client.ExecuteRpcBatchAsync( new RpcRequestMessage(1, "eth_getBalance", "0xd092cd556828f7a2f4db7eeb9fe3b261cd664350", "latest"));

            Assert.AreEqual(1, result.Count, "incorrect result count");
            Assert.AreEqual(1, result[0].IdAsInteger(), "incorrect id");
            Assert.IsNotNull(result[0].Result, "result should be not null");
            Assert.IsNull(result[0].Error, "result should be null");
            
            Console.WriteLine(result.Count);
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }

        [Test]
        public async Task ExecutePost_DifferentCalls()
        {
            var client = new LykkeJsonRpcClient(NodeUrl);
            
            var guid = Guid.NewGuid();

            var result = await client.ExecuteRpcBatchAsync(
                new RpcRequestMessage(1, "eth_getBalance", "0xd092cd556828f7a2f4db7eeb9fe3b261cd664350", "latest"),
                new RpcRequestMessage("hello world", "eth_blockNumber"),
                new RpcRequestMessage(guid, "eth_blockNumber"));

            Assert.AreEqual(3, result.Count, "incorrect result count");
            Assert.AreEqual(1, result[0].IdAsInteger(), "incorrect id 1");
            Assert.AreEqual("hello world", result[1].IdAsString(), "incorrect id 2");
            Assert.AreEqual(guid, result[2].IdAsGuid(), "incorrect id 3");

            Console.WriteLine(result.Count);
            Console.WriteLine(JsonConvert.SerializeObject(result));

            Console.WriteLine(result[1].DataAsString());
        }

        [Test]
        public async Task GetBalance()
        {
            var client = new LykkeJsonRpcClient(NodeUrl);

            var balance = await client.GetEtherBalanceAsync("0xd092cd556828f7a2f4db7eeb9fe3b261cd664350");

            Assert.AreEqual(0, balance);
        }

        [Test]
        public async Task GetBlockNumber()
        {
            var client = new LykkeJsonRpcClient(NodeUrl);

            var blockNumber = await client.GetBlockNumberAsync();

            Console.WriteLine(blockNumber);
        }

        [Test]
        public async Task GetTransactionByHash()
        {
            var client = new LykkeJsonRpcClient(NodeUrl);
            var result = await client.ExecuteRpcBatchAsync(
                new RpcRequestMessage(1, ApiMethodNames.eth_getTransactionByHash, "0x99e1ad3ff1508446b29818c08d95926f6f2636afdd85400290cae43419b4fce6"),
                new RpcRequestMessage(2, ApiMethodNames.eth_getTransactionByHash, "0xa401c9633256cf769f1d0d2d3af74711d71b106bb6f04e56f87518a6c3526fb0"),
                new RpcRequestMessage(3, ApiMethodNames.eth_getTransactionByHash, "0x24e3c0765dbb0c073526ca22ece466e58500475fc50f07ec196c7399fb93913d"));

            Assert.AreEqual(3, result.Count);

            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }
    }
}
