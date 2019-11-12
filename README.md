# Lykke.Nethereum.Extension
The extension to Nethereum library

# Batch execute via https

1. Create RPC client: 
ILykkeJsonRpcClientvar client = new LykkeJsonRpcClient(NodeUrl);

2. Make a call:
var result = await client.ExecuteRpcBatchAsync(
    new RpcRequestMessage(1, "eth_getBalance", "0xd092cd556828f7a2f4db7eeb9fe3b261cd664350", "latest"),
    new RpcRequestMessage("hello world", "eth_blockNumber"));

3. Handle results
Assert.AreEqual(2, result.Count);
Assert.AreEqual(1, result[0].IdAsInteger());
Assert.AreEqual("hello world", result[1].IdAsString());

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
