Console.WriteLine("=== 实验五:多型 ===");

// ⭐ 一个 List,装两种不同的帐户
List<BankAccount> accounts = new List<BankAccount>
{
    new SavingsAccount("Alice", 1000),
    new CheckingAccount("Bob", 1000),
    new SavingsAccount("Carol", 3000),
};

foreach (BankAccount acc in accounts)
{
    try
    {
        acc.Withdraw(1500);                      // ⭐ 同一行程式码
        Console.WriteLine($"{acc.Owner} 提款成功,余额 {acc.Balance}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{acc.Owner} 提款失败:{ex.Message}");
    }
}

var bob = new CheckingAccount("Bob2", 1000);
bob.Withdraw(1500);                              // 变数型别是 CheckingAccount
Console.WriteLine($"直接呼叫:{bob.Balance}");

BankAccount bobAsBase = bob;                     // 同一个物件,换个型别看
Console.WriteLine($"—— 现在用 BankAccount 型别呼叫 ——");
class BankAccount
{
    public string Owner { get; }
    public decimal Balance { get; protected set; }

    public BankAccount(string owner, decimal initialDeposit)
    {
        if (string.IsNullOrWhiteSpace(owner)) throw new ArgumentException("户名不能空白");
        if (initialDeposit < 0) throw new ArgumentException("开户金额不能为负");
        Owner = owner;
        Balance = initialDeposit;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("存款金额必须大于 0");
        Balance += amount;
    }

    public virtual void Withdraw(decimal amount)          // ⭐ virtual
    {
        if (amount <= 0) throw new ArgumentException("提款金额必须大于 0");
        if (amount > Balance) throw new InvalidOperationException("余额不足");
        Balance -= amount;
    }
}

class SavingsAccount : BankAccount
{
    public SavingsAccount(string owner, decimal initialDeposit)
        : base(owner, initialDeposit) { }
    // 不覆写 Withdraw —— 直接用父类别那套
}

class CheckingAccount : BankAccount
{
    private const decimal OverdraftLimit = -1000m;

    public CheckingAccount(string owner, decimal initialDeposit)
        : base(owner, initialDeposit) { }

    public override void  Withdraw(decimal amount)        // ⭐ override
    {
        if (amount <= 0) throw new ArgumentException("提款金额必须大于 0");
        if (Balance - amount < OverdraftLimit)
            throw new InvalidOperationException("超过透支额度");
        Balance -= amount;
    }
}