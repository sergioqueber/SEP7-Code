using System.Diagnostics;

namespace Model;

public class Report
{
    public int Id { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public List<User>? ActiveParticipants { get; set; }
    public List<Reward>? AwardedRewards { get; set; }
    public int EmissionsSaved { get; set; }
    public List<Activity>? CompletedActivities { get; set; }

    public Report() { }
    public Report(int id, DateOnly startDate, DateOnly endDate, List<User> activeParticipants, List<Reward> awarededRewards, int emissionsSaved, List<Activity>? completedActivities)
    {
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
        ActiveParticipants = activeParticipants;
        AwardedRewards = awarededRewards;
        EmissionsSaved = emissionsSaved;
        CompletedActivities = completedActivities;
    }
}