using Dapper;
using Npgsql;
using System.ComponentModel.DataAnnotations;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Dbfunctions dbfunctions = new Dbfunctions();
dbfunctions.GetById(2);

class Dbfunctions : IDbfunctions
{
    Action<Actor> ShowActor = (Actor actor) => Console.WriteLine($"Name: {actor.first_name}, Lastname: {actor.last_name}, Lastupdate: {actor.last_updated}");
    
    
    private readonly string connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=root;Database=DVD store";
    public void Add()
    {
        using(var connection = new NpgsqlConnection(connectionString))
        {
            var newactor = new Actor()
            {
                actor_id = 10000,
                first_name = "Leonardo",
                last_name = "Di Cabrio",
                last_updated = DateTime.Now,

            };
            var sqlQuery = "Insert into actor values(@actor_id, @first_name, @last_name, @last_update)";
            connection.Execute(sqlQuery, newactor);
        }
    }

    

    public void DeleteAll()
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            string sql = $"Delete FROM actor";
            connection.Query<Actor>(sql); 
        }
    }

    public void DeleteById(int id)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            string sql = $"Delete FROM actor where actor_id={id}";
            connection.Query(sql);
        }
    }

    public void DeleteByName(string name)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            string sql = $"Delete FROM actor where first_name={name}";
            connection.Query(sql);
        }
    }

    public void GetById(int id)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            string sql = $"Select * from actor where actor_id={id}";
            var actor = connection.QueryFirst<Actor>(sql);
            ShowActor((Actor)actor);
        }
    }

    public void GetByName(string name)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            string sql = $"Select * from actor where actor_id={name}";
            var actor = connection.QueryFirst<Actor>(sql);
            ShowActor(actor);
        }
    } 
}


class Actor
{
    [Key]
    public int actor_id { get; set; }
    [Display(Name ="Firstname")]
    public string first_name { get; set; }
    public string last_name { get; set; }
    public DateTime? last_updated { get; set; }
}






interface IDbfunctions
{
    void Add();
    void DeleteAll();
    void DeleteById(int id);
    void DeleteByName(string name);
    void GetById(int id);
    void GetByName(string name);
}