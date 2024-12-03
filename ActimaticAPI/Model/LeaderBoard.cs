namespace Model;
public class LeaderBoard
{
    public List<User> Ranking { get; set; }
    public LeaderBoard(List<User> ranking)
    {
        Ranking = ranking;
    }
}