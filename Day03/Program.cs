Console.WriteLine("=== 实验五:static ===");

var a = new BankAccount("Alice", 1000);
var b = new BankAccount("Bob", 500);
var c = new BankAccount("Carol", 2000);

Console.WriteLine($"Alice 余额:{a.Balance}");
Console.WriteLine($"Bob   余额:{b.Balance}");
Console.WriteLine($"总共开了几个帐户:{BankAccount.AccountCount}");   // ⭐ 不是 a.AccountCount

class BankAccount
{
    private static int _accountCount = 0;                    // ⭐ static
    public static int AccountCount => _accountCount;         // ⭐ static

    public string Owner { get; }
    public decimal Balance { get; private set; }

    public BankAccount(string owner, decimal initialDeposit)
    {
        if (string.IsNullOrWhiteSpace(owner))
            throw new ArgumentException("户名不能空白");
        if (initialDeposit < 0)
            throw new ArgumentException("开户金额不能为负");

        Owner = owner;
        Balance = initialDeposit;
        _accountCount++;                                      // ⭐ 每开一个就 +1
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("存款金额必须大于 0");
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("提款金额必须大于 0");
        if (amount > Balance) throw new InvalidOperationException("余额不足");
        Balance -= amount;
    }
}