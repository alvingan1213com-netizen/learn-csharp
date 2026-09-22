Console.WriteLine("=== 实验五:record ===");

var r1 = new PersonRecord("Alice", 30);
var r2 = new PersonRecord("Alice", 30);

Console.WriteLine($"r1 == r2 : {r1 == r2}");
Console.WriteLine($"r1.Equals(r2) : {r1.Equals(r2)}");
Console.WriteLine($"号码牌一样吗:{r1.GetHashCode() == r2.GetHashCode()}");
Console.WriteLine($"印出来:{r1}");

var set = new HashSet<PersonRecord> { r1, r2 };
Console.WriteLine($"集合里有几个:{set.Count}");

// ⭐ 想改年龄?
var older = r1 with { Age = 31 };
Console.WriteLine($"原本的:{r1}");
Console.WriteLine($"新的:{older}");

class Person
{

        public override int GetHashCode() => HashCode.Combine(Name, Age);

        public override bool Equals(object? obj)
    {
        if (obj is not Person other) return false;      // 不是 Person 就不相等
        return Name == other.Name && Age == other.Age;  // ⭐ 我决定:名字+年龄一样就算一样
    }
    public override string ToString() => $"{Name}({Age} 岁)";
    public string Name { get; }
    public int Age { get; }
    

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

record PersonRecord(string Name, int Age);