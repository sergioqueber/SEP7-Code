namespace Model;

public class RedeemedReward{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public int UserId { get; set; }
    public int RewardId { get; set; }
    public int? TeamId { get; set; }

    public User? User { get; set; }
    public Team? Team { get; set; }

    public RedeemedReward() { }
}