Console.WriteLine("=== 实验一:值型别 ===");

int a = 1;
int b = a;        // 影印一份给 b

b = 2;            // 改 b

Console.WriteLine($"a = {a}");
Console.WriteLine($"b = {b}");

Console.WriteLine("=== 实验二:参考型别 ===");

Person p1 = new Person();
p1.Name = "Alice";

Person p2 = p1;      // 这次复制的是什么?

p2.Name = "Bob";     // 改 p2

Console.WriteLine($"p1.Name = {p1.Name}");
Console.WriteLine($"p2.Name = {p2.Name}");
Console.WriteLine($"是同一个物件吗? {ReferenceEquals(p1, p2)}");


Console.WriteLine("=== 实验三:传进方法 ===");

// 值型别
int number = 10;
ChangeNumber(number);
Console.WriteLine($"number = {number}");

// 参考型别
Person person = new Person();
person.Name = "Alice";
ChangePerson(person);
Console.WriteLine($"person.Name = {person.Name}");

Console.WriteLine("=== 实验四:重新指派 ===");


person.Name = "Alice";

ReplacePerson(person);

Console.WriteLine($"person.Name = {person.Name}");
Console.WriteLine("=== 实验五:string ===");

string s = "Alice";
ChangeString(s);
Console.WriteLine($"s = {s}");

void ChangeString(string text)
{
    text = "Bob";
}

void ReplacePerson(Person p)
{
    p = new Person();      // ⭐ 注意这行:整个换掉
    p.Name = "Bob";
}



void ChangeNumber(int n)
{
    n = 999;
}

void ChangePerson(Person p)
{
    p.Name = "Bob";
}

class Person
{
    public string Name = "";
}