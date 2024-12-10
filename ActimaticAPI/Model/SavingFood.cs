namespace Model;
public class SavingFood : Activity
{
   public int PackagesSaved { get; set; }
   
   public SavingFood() { }
    public SavingFood(int id, string name, int awardedPoints, DateOnly date, int packagesSaved) : base(id, name, awardedPoints, date)
    {
       PackagesSaved = packagesSaved;
    }
}