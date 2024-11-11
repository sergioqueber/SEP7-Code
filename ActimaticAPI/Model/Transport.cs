namespace Model;
public class Transport: Activity
{
    public int Distance {get; set;}
    public string Type {get; set;}
    public int EmissionsSaved {get; set;}
    public Transport (int id, string name, int awardedPoints, DateTime date, int distance, string type, int emissionsSaved ): base(id, name, awardedPoints, date)
    {
        Id = id;
        Name = name;
        AwardedPoints = awardedPoints;
        Date = date;
        Distance = distance;
        Type = type;
        EmissionsSaved = emissionsSaved;
    }
}

