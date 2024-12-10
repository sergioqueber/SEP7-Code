namespace Model;
public class Stairs : Activity
{
    public int Floors { get; set; }

    public Stairs() { }
    public Stairs(int id, string name, int awardedPoints, DateOnly date, int floors) : base(id, name, awardedPoints, date)
    {
        Floors = floors;
    }
}