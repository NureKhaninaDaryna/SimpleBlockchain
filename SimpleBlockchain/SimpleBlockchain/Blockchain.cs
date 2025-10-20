namespace SimpleBlockchain;

public class Blockchain
{
    public List<Block> Chain { get; set; } = [CreateGenesisBlock()];
    public int Difficulty { get; set; } = 4;

    private static Block CreateGenesisBlock()
    {
        var genesisData = new Dictionary<string, string> { { "genesis", "Початковий блок" } };
        return new Block(0, genesisData, "0");
    }
    
    public Block GetLatestBlock() => Chain[^1];
    
    public void AddBlock(Block newBlock)
    {
        newBlock.PreviousHash = GetLatestBlock().Hash;
        MineBlock(newBlock);
        Chain.Add(newBlock);
    }
    
    public void MineBlock(Block block)
    {
        var target = new string('0', Difficulty);
        while (true)
        {
            block.Hash = block.CalculateHash();
            if (block.Hash[..Difficulty] == target)
                break;
            block.Nonce++;
        }
        Console.WriteLine($"Miner found hash: {block.Hash} with nonce {block.Nonce}");
    }
    
    public bool IsValid()
    {
        for (var i = 1; i < Chain.Count; i++)
        {
            var current = Chain[i];
            var previous = Chain[i - 1];
            
            if (current.Hash != current.CalculateHash())
                return false;
            if (current.PreviousHash != previous.Hash)
                return false;
        }
        return true;
    }
}