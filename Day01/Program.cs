// See https://aka.ms/new-console-template for more information
class Order { }              // ❌ 先定义了 class
Console.WriteLine("hi");     // 顶层陈述式在后面
Console.WriteLine("Hello, World!");
Console.WriteLine("我是 B 版");

int Add(int a, int b) => a + b;
Console.WriteLine($"3 + 5 = {Add(3, 5)}");
int Subtract(int a, int b) => a - b;
Console.WriteLine($"9 - 4 = {Subtract(9, 4)}");
int Multiply(int a, int b) => a * b;
Console.WriteLine($"6 * 3 = {Multiply(6, 3)}");
int Divide(int a, int b)
{
    if (b == 0) throw new DivideByZeroException("除数不能为 0");
    return a / b;
}
Console.WriteLine($"10 / 2 = {Divide(10, 2)}");
Console.WriteLine($"收到 {args.Length} 个参数");
foreach (var a in args) Console.WriteLine($"  - {a}");