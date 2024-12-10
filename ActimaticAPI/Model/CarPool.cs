namespace Model;
public class CarPool: Activity
{
    public int Distance {get; set;}
    public int EmptySeats {get; set;}
    public int FilledSeats {get; set;}
    public string CarType {get; set;}
    
    public CarPool() { }
    public CarPool (int id, string name, int awardedPoints, DateOnly date, int distance, int emptySeats, int filledSeats, string carType): base(id, name, awardedPoints, date)
    {
        Id = id;
        Name = name;
        AwardedPoints = awardedPoints;
        Date = date;
        Distance = distance;
        EmptySeats = emptySeats;
        FilledSeats = filledSeats;
        CarType = carType;
    }
}