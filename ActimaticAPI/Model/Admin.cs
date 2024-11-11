namespace Model;
public class Admin : User
{
    public List<Staff> toApprove { get; set; }
    public Admin(string name, string surname, string email, string password) : base(name, surname, email, password)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
    }
}