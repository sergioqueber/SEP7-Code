using System.Diagnostics;

namespace Model;

public class Report{
    public int Id {get; set;}
    public DateTime StartDate {get; set;}
    public DateTime EndDate {get; set;}
    public List<Staff>? ActiveParticipants {get; set;}
    public List<Reward>? AwardedRewards {get; set;}
    public int EmissionsSaved {get; set;}
    public List<Activity>? CompletedActivities {get; set;}

    public Report(int id, DateTime startDate, DateTime endDate, List<Staff> activeParticipants, List<Reward> awaredeRewards, int emissionsSaved, List<Activity>? completedActivities){
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
        ActiveParticipants = activeParticipants;
        AwardedRewards = awaredeRewards;
        EmissionsSaved = emissionsSaved;
        CompletedActivities = completedActivities;
    }
}