Console.WriteLine();
Console.WriteLine("=== 实验六:InnerException ===");

try
{
    var repo = new OrderRepository();
    repo.Save("ORD-001");
}
catch (Exception ex)
{
    Console.WriteLine($"外层:{ex.GetType().Name} - {ex.Message}");
    Console.WriteLine($"内层:{ex.InnerException?.GetType().Name} - {ex.InnerException?.Message}");
    Console.WriteLine();
    Console.WriteLine("完整内容:");
    Console.WriteLine(ex.ToString());          // ⭐ 看看它印出什么
}

class OrderRepository
{
    public void Save(string orderId)
    {
        try
        {
            throw new TimeoutException("资料库连线逾时");    // 假装底层出事
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"储存订单 {orderId} 失败", ex);   // ⭐ 包起来
        }
    }
}