# Lykke.Nethereum.Extension
The extension to Nethereum library

# Batch execute via https

1. Create RPC client: 
ILykkeJsonRpcClientvar client = new LykkeJsonRpcClient(NodeUrl);

2. Make a call:
var result = await client.ExecuteRpcBatchAsync(
    new RpcRequestMessage(1, ApiMethodNames.eth_getBalance, "0xd092cd556828f7a2f4db7eeb9fe3b261cd664350", "latest"),
    new RpcRequestMessage("hello world", ApiMethodNames.eth_blockNumber),
    new RpcRequestMessage(guid, ApiMethodNames.eth_blockNumber));

Assert.AreEqual(3, result.Count);
Assert.AreEqual(1, result[0].IdAsInteger());
Assert.AreEqual("hello world", result[1].IdAsString());
Assert.AreEqual(guid, result[2].IdAsGuid());

# Get block number

1. Create RPC client: 
ILykkeJsonRpcClientvar client = new LykkeJsonRpcClient(NodeUrl);

2. Make a call:
Int64 blockNumber = await client.GetBlockNumberAsync();

3. Handle results
Console.WriteLine($"Last block number: {blockNumber}");

# Get Ether balance

1. Create RPC client: 
ILykkeJsonRpcClientvar client = new LykkeJsonRpcClient(NodeUrl);

2. Make a call:
Int balanceWei = wait client.GetEtherBalanceAsync("0xd092cd556828f7a2f4db7eeb9fe3b261cd664350");

3. Handle results
Console.WriteLine($"balance in wei: {balanceWei}");

# Get Transaction Receipt

1. Create RPC client: 
ILykkeJsonRpcClientvar client = new LykkeJsonRpcClient(NodeUrl);

2. Make a call:
var receipt = await client.GetTransactionReceiptAsync("0xee6acd2754dce87a5d5a4ca8ce366a00b8ae3917039eb3ad9179ef6d9eae2591");

3. Handle results
Assert.IsTrue(1 == receipt.Status.Value);
