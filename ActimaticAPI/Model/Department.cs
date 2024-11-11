using System.Collections.Specialized;

namespace Model;

public class Department{
    public string Name {get; set;}
    public int Id {get; set;}
    public Department(string name, int id){
        Name = name;
        Id = id;
    }
}