namespace Model;
public class CarPool: Activity
{
    public int Distance {get; set;}
    public int EmptySeats {get; set;}
    public string CarType {get; set;}
}

public CarPool (int id, string name, int awardedPoints, DateTime date, int distance, int emptySeats, string carType): base(id, name, awardedPoints, date)
{
    Id = id;
    Name = name;
    AwardedPoints = awardedPoints;
    Date = date;
    Distance = distance;
    EmptySeats = emptySeats;
    CarType = carType;
}