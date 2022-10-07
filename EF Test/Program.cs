using Microsoft.EntityFrameworkCore;
//TODO Создать класс "Session"
//using (ApplicationContext db = new ApplicationContext())
//{
//    db.Database.EnsureDeleted();
//    db.Database.EnsureCreated();
//    // создаем поле
//    Field BoysField = new Field { Length = 50, Width = 40, Name = "For Boys!" };
//    Field GirlsField = new Field { Length = 20, Width = 40, Name = "For Girls!" };

//    // добавление в базу
//    db.Fields.AddRange(BoysField, GirlsField);

//    // создаем два объекта User

//    User Sanya = new User { Name = "Sanya", Age = 52, Field = BoysField };
//    User Serega = new User { Name = "Serega", Age = 20, Field = BoysField };
//    db.Users.AddRange(Sanya, Serega);

//    BoysField.Players.Insert(0, Sanya);
//    BoysField.Players.Insert(1, Serega);

//    //User Sanya = new User { Name = "Sanya", Age = 52, Field = BoysField };
//    //User Serega = new User { Name = "Serega", Age = 20, Field = BoysField };
//    //User Vanya = new User { Name = "Vanya", Age = 21, Field = GirlsField };
//    //User Vlad = new User { Name = "Vlad", Age = 24, Field = BoysField };

//    // сохранить изменения
//    db.SaveChanges();

//    //выборка плееров из поля для Boys
//    //получаем объекты из бд (таблица юзеров -> переход в таблицу Field -> Проверяем на поле "For Boys")
//    //var users = db.Users.Include(u => u.Field).Where(u => u.Field.Name == "For Boys!").ToList();
//    var information = db.Fields.Include(u => u.Players).Where(u => u.Name == "For Boys!").ToList();


//    foreach (var info in information)
//    {
//        Console.WriteLine($"{info.Players[0].Name} VS {info.Players[1].Name} ");
//    }

//}

Field field = new Field { Length = 50, Width = 40, Name = "NefedovBattle" };
User Player1 = new User { Name = "Vanya", Age = 52 };
User Player2 = new User { Name = "Timur", Age = 20 };

using (ApplicationContext db = new())
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    db.Users.AddRange(Player1, Player2);
    db.Fields.Add(field);
    field.Players.Insert(0, Player1);
    field.Players.Insert(1, Player2);
    db.SaveChanges();
}

using (ApplicationContext db = new())
{
    //var Users = db.Users.Include(u => u.Field).ToList();
    //foreach (var item in Users)
    //{
    //    Console.WriteLine($"{item.Name} - {item.Field.Name}");
    //}

    var SessionInfo = db.Fields.Include(u => u.Players).ToList();
    foreach (var item in SessionInfo)
    {

        Console.WriteLine($"Field: {item.Length} x {item.Width}:\n{item.Players[0]} VS {item.Players[1]}" );

    }
}


//
public class User
{

    public int UserId { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public Field Field { get; set; }

}

public class Field
{
    public int FieldId { get; set; }
    //TODO enum для шаблонов field-а
    public string? Name { get; set; } = "Default";
    public int Length { get; set; }
    public int Width { get; set; }
    public List<User> Players { get; set; } = new List<User>(2);
}

public class ApplicationContext : DbContext
{

    public DbSet<User> Users => Set<User>();
    public DbSet<Field> Fields => Set<Field>();

    public ApplicationContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=helloapp.db");
    }

}

