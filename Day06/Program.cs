Console.WriteLine("=== 实验六:吞掉例外 ===");

var order = new Order();

try
{
    order.Total = -100;
}
catch (Exception)
{
    // 什么都不做
}

Console.WriteLine($"订单金额:{order.Total}");
Console.WriteLine("流程继续...存进资料库...");

class Order
{
    private decimal _total;
    public decimal Total
    {
        get => _total;
        set
        {
            if (value < 0) throw new ArgumentException("金额不能为负");
            _total = value;
        }
    }
}