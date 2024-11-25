namespace Model;
public class Staff : User
{
    public string Role {get; set;}
    public Department Department {get; set;}
    public int Points {get; set;}
    public List<Activity> Activities {get; set;}
    public List<Reward> Rewards {get; set;}

    public Staff(int id, string name, string surname, string email, string password, string role, Department department, int points) : base(id, name, surname, email, password)
    {
        Role = role;
        Department = department;
        Points = points;
        Activities = new List<Activity>();
        Rewards = new List<Reward>();
    }
}