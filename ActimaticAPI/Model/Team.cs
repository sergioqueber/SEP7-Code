namespace Model;

public class Team{
    public int Id {get; set;}
    public string Name {get; set;}
    public Department Department {get; set;}

    public List<Reward> TeamRewards {get; set;}

    public Team(){}
    public Team(int id, string name, Department department, List<Reward> teamRewards) 
    {
        Id = id;
        Name = name;
        Department  = department;
        TeamRewards = teamRewards;
    }

}

