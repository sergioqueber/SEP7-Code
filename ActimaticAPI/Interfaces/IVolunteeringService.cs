namespace Interfaces;
using Model;
public interface IVolunteeringService
{
    Task<IEnumerable<Volunteering>> GetAllVolunteering();
    Task<Volunteering> GetVolunteeringById(int id);
    Task<Volunteering> CreateVolunteering(Volunteering volunteering);
    Task<Volunteering> UpdateVolunteering(Volunteering volunteering);
    Task<Volunteering> RemoveVolunteering(int id);
}