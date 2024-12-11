namespace Model;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Department? Department { get; set; }

    public int? DepartmentId { get; set; }
    public int TotalPoints { get; set; } = 0;
    public List<RedeemedReward>? TeamRewards { get; set; }
    public List<User>? Staff { get; set; } = [];
    public Team() { }
    public Team(int id, string name, Department department, List<Reward> teamRewards)
    {
        Id = id;
        Name = name;
        Department = department;
    }

}

