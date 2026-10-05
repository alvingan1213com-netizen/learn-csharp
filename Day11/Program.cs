using Microsoft.Data.Sqlite;

using var conn = new SqliteConnection("Data Source=shop.db");
conn.Open();

// ---- 建表 ----
Exec(@"
DROP TABLE IF EXISTS Orders;
DROP TABLE IF EXISTS Customers;

CREATE TABLE Customers (
    Id    INTEGER PRIMARY KEY,
    Name  TEXT NOT NULL,
    City  TEXT
);

CREATE TABLE Orders (
    Id         INTEGER PRIMARY KEY,
    CustomerId INTEGER NOT NULL,
    Product    TEXT NOT NULL,
    Amount     DECIMAL NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
);
");

// ---- 塞资料 ----
Exec(@"
INSERT INTO Customers (Id, Name, City) VALUES
    (1, 'Alice', '台北'),
    (2, 'Bob',   '台中'),
    (3, 'Carol', '高雄'),
    (4, 'David', NULL);        -- ⭐ David 没下过单,城市也是空的

INSERT INTO Orders (Id, CustomerId, Product, Amount) VALUES
    (1, 1, '笔电',  35000),
    (2, 2, '滑鼠',    500),
    (3, 1, '键盘',   1200),
    (4, 3, '萤幕',   8000),
    (5, 2, '笔电',  35000),
    (6, 1, '耳机',   2500);
");

Console.WriteLine("资料库建好了");

Query("城市不是台北的客户(用 != )", @"
    SELECT Id, Name, City FROM Customers WHERE City != '台北'
");

Query("城市不是台北的客户(正确写法)", @"
    SELECT Id, Name, City FROM Customers 
    WHERE City != '台北' OR City IS NULL
");

Query("NULL = NULL 吗?", @"
    SELECT 
        NULL = NULL      AS 'NULL=NULL',
        1 = 1            AS '1=1',
        NULL IS NULL     AS 'NULL IS NULL'
");


// ===== 以下是工具,先不用看懂 =====
void Exec(string sql)
{
    using var cmd = conn.CreateCommand();
    cmd.CommandText = sql;
    cmd.ExecuteNonQuery();
}

void Query(string title, string sql)
{
    Console.WriteLine($"--- {title} ---");
    using var cmd = conn.CreateCommand();
    cmd.CommandText = sql;
    using var reader = cmd.ExecuteReader();

    var cols = new List<string>();
    for (int i = 0; i < reader.FieldCount; i++) cols.Add(reader.GetName(i));
    Console.WriteLine("  " + string.Join(" | ", cols));

    int rows = 0;
    while (reader.Read())
    {
        var vals = new List<string>();
        for (int i = 0; i < reader.FieldCount; i++)
            vals.Add(reader.IsDBNull(i) ? "NULL" : reader.GetValue(i)?.ToString() ?? "");
        Console.WriteLine("  " + string.Join(" | ", vals));
        rows++;
    }
    Console.WriteLine($"  ({rows} 笔)\n");
}