namespace Model;
public class SavingFood : Activity
{
   public int PackagesSaved { get; set; }
   
    public SavingFood(int id, string name, int awardedPoints, DateTime date, int packagesSaved) : base(id, name, awardedPoints, date)
    {
       PackagesSaved = packagesSaved;
    }
}