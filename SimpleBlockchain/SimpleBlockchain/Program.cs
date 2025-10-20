namespace SimpleBlockchain;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;
        
        var chain = new Blockchain { Difficulty = 4 };
        
        // Додаємо перший блок
        var tx1 = new Dictionary<string, string> { { "від", "Alice" }, { "до", "Bob" }, { "сума", "10" } };
        var block1 = new Block(1, tx1, chain.GetLatestBlock().Hash);
        chain.AddBlock(block1);
        
        // Додаємо другий блок
        var tx2 = new Dictionary<string, string> { { "від", "Bob" }, { "до", "Charlie" }, { "сума", "5" } };
        var block2 = new Block(2, tx2, chain.GetLatestBlock().Hash);
        chain.AddBlock(block2);
        
        Console.WriteLine("\nЛанцюжок блоків:");
        foreach (var b in chain.Chain)
            Console.WriteLine(b);
        
        Console.WriteLine($"\nПеревірка цілісності ланцюга: {chain.IsValid()}");
        
        // Демонстрація модифікації даних і повторної перевірки
        Console.WriteLine("\n--- Модифікуємо транзакцію у блоці 1 ---");
        chain.Chain[1].Transactions["сума"] = "1000";
        Console.WriteLine($"Після зміни: Перевірка цілісності ланцюга: {chain.IsValid()}");
    }
}