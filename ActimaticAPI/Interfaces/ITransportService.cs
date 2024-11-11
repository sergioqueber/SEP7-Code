namespace Interfaces;
using Model;
public interface ITransportService
{
    Task<IEnumerable<Transport>> GetAllStaff();
    Task<Transport> GetTransportById(int id);
    Task<Transport> CreateTransport(Transport transport);
    Task<Transport> UpdateTransport(Transport transport);
    Task<Transport> RemoveTransport(int id);    

    
}