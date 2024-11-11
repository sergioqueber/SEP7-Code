namespace Model;
public class Admin : User
{
    public List<Staff> ToApprove { get; set; }
    public Admin(int id, string name, string surname, string email, string password, List<Staff> toApprove) : base(id, name, surname, email, password)
    {
      ToApprove = toApprove;
    }
}