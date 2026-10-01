var orders = new List<Order>
{
    new Order("ORD-001", "Alice", "笔电", 35000m, new DateTime(2026, 1, 15)),
    new Order("ORD-002", "Bob",   "滑鼠",   500m, new DateTime(2026, 1, 20)),
    new Order("ORD-003", "Alice", "键盘",  1200m, new DateTime(2026, 2, 3)),
    new Order("ORD-004", "Carol", "萤幕",  8000m, new DateTime(2026, 2, 10)),
    new Order("ORD-005", "Bob",   "笔电", 35000m, new DateTime(2026, 2, 14)),
    new Order("ORD-006", "Alice", "耳机",  2500m, new DateTime(2026, 3, 1)),
};

Console.WriteLine("=== 实验一:Where ===");

var bigOrders = orders.Where(o => o.Amount > 5000);

foreach (var o in bigOrders)
{
    Console.WriteLine($"  {o.Id} {o.Customer} {o.Amount}");
}

Console.WriteLine("=== 实验二:Select ===");

var names = orders.Select(o => o.Customer);
Console.WriteLine($"  客户:{string.Join(", ", names)}");

// ⭐ 也可以变形
var summaries = orders.Select(o => $"{o.Customer} 买了 {o.Product}");
foreach (var s in summaries.Take(3))
{
    Console.WriteLine($"  {s}");
}

Console.WriteLine("=== 实验三:串起来 ===");

var result = orders
    .Where(o => o.Amount > 1000)                   // 筛选
    .OrderByDescending(o => o.Amount)              // 排序(大→小)
    .Select(o => $"{o.Customer}: {o.Amount}");     // 变形

foreach (var r in result)
{
    Console.WriteLine($"  {r}");
}

Console.WriteLine("=== 实验四:单一结果 ===");

Console.WriteLine($"  总金额:{orders.Sum(o => o.Amount)}");
Console.WriteLine($"  笔数:{orders.Count()}");
Console.WriteLine($"  最大一笔:{orders.Max(o => o.Amount)}");
Console.WriteLine($"  平均:{orders.Average(o => o.Amount):F0}");

Console.WriteLine($"  有没有超过三万的:{orders.Any(o => o.Amount > 30000)}");
Console.WriteLine($"  是不是全部超过 100:{orders.All(o => o.Amount > 100)}");

var first = orders.First(o => o.Customer == "Bob");
Console.WriteLine($"  Bob 的第一笔:{first.Id}");

var notFound = orders.FirstOrDefault(o => o.Customer == "David");
Console.WriteLine($"  David 的第一笔:{(notFound is null ? "没有" : notFound.Id)}");

Console.WriteLine("=== 实验五:GroupBy ===");

var byCustomer = orders
    .GroupBy(o => o.Customer)                      // GROUP BY Customer
    .Select(g => new
    {
        Customer = g.Key,                          // 分组的依据
        Count = g.Count(),                         // COUNT(*)
        Total = g.Sum(o => o.Amount)               // SUM(Amount)
    })
    .OrderByDescending(x => x.Total);

foreach (var x in byCustomer)
{
    Console.WriteLine($"  {x.Customer}:{x.Count} 笔,共 {x.Total}");
}

Console.WriteLine("=== 实验六:什么时候执行 ===");

var list = new List<int> { 1, 2, 3 };

var query = list.Where(n =>
{
    Console.WriteLine($"    [检查 {n}]");      // ⭐ 每次比较都印一行
    return n > 1;
});

Console.WriteLine("  查询已经建立了,上面有印出东西吗?");
Console.WriteLine("  ---- 现在开始 foreach ----");

foreach (var n in query)
{
    Console.WriteLine($"  拿到 {n}");
}
Console.WriteLine("  ---- 改了原始资料之后 ----");
list.Add(99);

foreach (var n in query)          // ⭐ 同一个 query,再跑一次
{
    Console.WriteLine($"  拿到 {n}");
}

var snapshot = list.Where(n => n > 1).ToList();    // ⭐ ToList 立刻执行
record Order(string Id, string Customer, string Product, decimal Amount, DateTime Date);