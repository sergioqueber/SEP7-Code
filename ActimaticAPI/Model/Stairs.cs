namespace Model;
public class Stairs : Activity
{
    public int Floors { get; set; }

    public Stairs(int id, string name, int awardedPoints, DateTime date, int floors) : base(id, name, awardedPoints, date)
    {
        Floors = floors;
    }
}