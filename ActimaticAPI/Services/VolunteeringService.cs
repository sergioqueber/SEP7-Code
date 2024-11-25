using Interfaces;
using Model;

namespace Services;
public class VolunteeringService : IVolunteeringService
{
    private static List<Volunteering> volunteeringList = new List<Volunteering>();

    static VolunteeringService()
    {
        
    }

    public Task<IEnumerable<Volunteering>> GetAllVolunteering()
    {
        return Task.FromResult(volunteeringList.AsEnumerable());
    }

    public Task<Volunteering> GetVolunteeringById(int id)
    {
        var volunteering = volunteeringList.FirstOrDefault(v => v.Id == id);
        return Task.FromResult(volunteering);
    }

    public Task<Volunteering> CreateVolunteering(Volunteering volunteering)
    {
        volunteeringList.Add(volunteering);
        return Task.FromResult(volunteering);
    }

    public Task<Volunteering> UpdateVolunteering(Volunteering volunteering)
    {
        var existingVolunteering = volunteeringList.FirstOrDefault(v => v.Id == volunteering.Id);
        if (existingVolunteering != null)
        {
            existingVolunteering.Name = volunteering.Name;
            existingVolunteering.AwardedPoints = volunteering.AwardedPoints;
            existingVolunteering.Date = volunteering.Date;
            existingVolunteering.Location = volunteering.Location;
            existingVolunteering.Hours = volunteering.Hours;
        }
        return Task.FromResult(existingVolunteering);
    }

    public Task<Volunteering> RemoveVolunteering(int id)
    {
        var volunteering = volunteeringList.FirstOrDefault(v => v.Id == id);
        if (volunteering != null)
        {
            volunteeringList.Remove(volunteering);
        }
        return Task.FromResult(volunteering);
    }
}