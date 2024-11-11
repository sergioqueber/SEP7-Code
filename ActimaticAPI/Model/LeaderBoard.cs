namespace Model;
public class LeaderBoard{
    public List<Staff> Ranking {get; set;}
    public LeaderBoard(List<Staff> ranking){
        Ranking = ranking;
    }
}