Console.WriteLine("=== 实验五:换掉实作 ===");

Console.WriteLine("--- 正式环境 ---");
var realService = new OrderService(new RealEmailSender());
realService.PlaceOrder("alice@example.com");

Console.WriteLine("--- 测试环境 ---");
var fake = new FakeEmailSender();
var testService = new OrderService(fake);
testService.PlaceOrder("bob@example.com");
testService.PlaceOrder("carol@example.com");

Console.WriteLine($"[验证] 测试中总共寄了 {fake.SentCount} 封信");

interface IEmailSender
{
    void Send(string to, string subject);
}

class RealEmailSender : IEmailSender
{
    public void Send(string to, string subject)
    {
        Console.WriteLine($"  (连线 SMTP 伺服器...真的寄给 {to}:{subject})");
    }
}

class FakeEmailSender : IEmailSender
{
    public int SentCount { get; private set; }

    public void Send(string to, string subject)
    {
        SentCount++;
        Console.WriteLine($"  (假装寄给 {to},没有真的连线)");
    }
}

class OrderService
{
    private readonly IEmailSender _sender;          // ⭐ 型别是 interface

    public OrderService(IEmailSender sender)        // ⭐ 从外面拿进来
    {
        _sender = sender;
    }

    public void PlaceOrder(string customerEmail)
    {
        Console.WriteLine($"订单已建立:{customerEmail}");
        _sender.Send(customerEmail, "您的订单已成立");
    }
}