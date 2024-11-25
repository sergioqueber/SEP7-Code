namespace Model;
public class Volunteering : Activity
{
    public string Location { get; set; }
    public int Hours { get; set; }

    public Volunteering(int id, string name, int awardedPoints, DateOnly date, string location, int hours ) : base(id, name, awardedPoints, date)
    {
        Location = location;
        Hours = hours;
    }
}