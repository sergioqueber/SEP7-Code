namespace Model;
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public int Points { get; set; }
    public bool IsApproved { get; set; } = false;
    public List<Activity>? Activities { get; set; } = [];
    public List<RedeemedReward>? Rewards { get; set; } = [];
    public Team? Team { get; set; }
    public List<Report>? Reports { get; set; } = [];

    public User() { }

    public User(int id, string name, string surname, string email, string password, string role, int points, bool IsApproved)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
        Role = role;
        Points = points;
        this.IsApproved = IsApproved;
    }
}