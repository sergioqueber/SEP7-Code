namespace Interfaces;
using Model;
public interface IVolunteeringService
{
    Task<IEnumerable<Volunteering>> GetAllVolunteeringAsync();
    Task<Volunteering> GetVolunteeringByIdAsync(int id);
    Task<Volunteering> CreateVolunteeringAsync(Volunteering volunteering);
    Task<Volunteering> UpdateVolunteeringAsync(Volunteering volunteering);
    Task<Volunteering> RemoveVolunteeringAsync(int id);
    Task<IEnumerable<Volunteering>> GetVolunteeringByDatesAsync(DateOnly startDate, DateOnly endDate);
}