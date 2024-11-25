using Interfaces;
using Model;

namespace Services;
public class TransportService : ITransportService{

    private static List<Transport> transportList = new List<Transport>();

    static TransportService(){

    }

    public Task<Transport> CreateTransport(Transport transport)
    {
        transportList.Add(transport);
        return Task.FromResult(transport);
    }
    
    public Task<IEnumerable<Transport>> GetAllTransport()
    {
        return Task.FromResult(transportList.AsEnumerable());
    }

    public Task<Transport> GetTransportById(int id)
    {
        return Task.FromResult(transportList.FirstOrDefault(x => x.Id == id));
    }

    public Task<Transport> RemoveTransport(int id)
    {
        var transport = transportList.FirstOrDefault(x => x.Id == id);
        transportList.Remove(transport);
        return Task.FromResult(transport);
    }

    public Task<Transport> UpdateTransport(Transport transport)
    {
        var transportToUpdate = transportList.FirstOrDefault(x => x.Id == transport.Id);
        transportToUpdate.Name = transport.Name;
        transportToUpdate.AwardedPoints = transport.AwardedPoints;
        transportToUpdate.Date = transport.Date;
        transportToUpdate.Distance = transport.Distance;
        transportToUpdate.Type = transport.Type;
        transportToUpdate.EmissionsSaved = transport.EmissionsSaved;
        return Task.FromResult(transportToUpdate);
    }
    
}