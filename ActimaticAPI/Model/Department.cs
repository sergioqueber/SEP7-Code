using System.Collections.Specialized;

namespace Model;

public class Department{
    public string Name {get; set;}
    public int Id {get; set;}
    public List<Team>? Teams {get; set;} =[];
    public Department(string name, int id){
        Name = name;
        Id = id;
    }
}