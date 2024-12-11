namespace Model;

public class Reward
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public int PointsRequired { get; set; }

    public bool Availability { get; set; }
    public List<RedeemedReward> RedeemedRewards { get; set; } = [];
    public List<Team> Teams { get; set; } = [];

    public Reward() { }


    public Reward(int id, string name, string description, int pointsRequired, bool availability)
    {
        Id = id;
        Name = name;
        Description = description;
        PointsRequired = pointsRequired;
        Availability = availability;
    }
}