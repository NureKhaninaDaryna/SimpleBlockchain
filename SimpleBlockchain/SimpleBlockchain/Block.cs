using System.Security.Cryptography;
using System.Text;

namespace SimpleBlockchain;

public class Block
{
    public int Index { get; set; }
    public DateTime Timestamp { get; set; }
    public Dictionary<string, string> Transactions { get; set; }
    public string PreviousHash { get; set; }
    public long Nonce { get; set; }
    public string Hash { get; set; }


    public Block(int index, Dictionary<string, string> transactions, string previousHash)
    {
        Index = index;
        Timestamp = DateTime.UtcNow;
        Transactions = transactions;
        PreviousHash = previousHash;
        Nonce = 0;
        Hash = CalculateHash();
    }


    public string CalculateHash()
    {
        using var sha256 = SHA256.Create();
        
        var rawData = Index + Timestamp.ToString("o") + TransactionsToString() + PreviousHash + Nonce;
        var bytes = Encoding.UTF8.GetBytes(rawData);
        var hashBytes = sha256.ComputeHash(bytes);
        var sb = new StringBuilder();
        foreach (var b in hashBytes)
            sb.Append(b.ToString("x2"));
        return sb.ToString();
    }


    private string TransactionsToString()
    {
        var sb = new StringBuilder();
        foreach (var kvp in Transactions)
        {
            sb.Append(kvp.Key).Append(':').Append(kvp.Value).Append(';');
        }
        return sb.ToString();
    }


    public override string ToString()
    {
        var prevHashShort = PreviousHash[..Math.Min(8, PreviousHash.Length)];
        var hashShort = Hash[..Math.Min(8, Hash.Length)];

        return $"Block(Index={Index}, Timestamp={Timestamp:o}, PrevHash={prevHashShort}..., Hash={hashShort}..., Nonce={Nonce})";
    }
}