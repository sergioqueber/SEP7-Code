namespace Model;
public abstract class User{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public User(int id, string name, string surname, string email, string password)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
    }
}

