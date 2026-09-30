Console.WriteLine("=== 实验六:泛型方法 ===");

Console.WriteLine(Max(3, 7));
Console.WriteLine(Max("apple", "banana"));
Console.WriteLine(Max(2.5, 1.8));

// ⭐ T 必须「能比大小」,所以要加约束
T Max<T>(T a, T b) where T : IComparable<T>
{
    return a.CompareTo(b) > 0 ? a : b;
}