namespace Model;

public abstract class Activity 
{
    public int Id {get; set;}
    public string Name {get; set;}
    public int AwardedPoints {get; set}
    public DateTime Date {get; set;}


    public Activity(int id, string name, int awardedPoints, DateTime date)
    {
        Id = id;
        Name = name;
        AwardedPoints = awardedPoints;
        Date = date;
    }
}