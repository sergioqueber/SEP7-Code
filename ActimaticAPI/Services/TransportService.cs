using Interfaces;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Storage;
using Microsoft.EntityFrameworkCore;


namespace Services;
public class TransportService(ApplicationDbContext context) : ITransportService
{
    private readonly ApplicationDbContext _context = context;
public class TransportService(ApplicationDbContext context) : ITransportService
{
    private readonly ApplicationDbContext _context = context;

    static TransportService()
    {
    }
    private const int BasePointsPerKm = 1;

    private int CalculatePoints(Transport transport)
    {
        int basepoints =  transport.Distance * BasePointsPerKm;
        int bonusPoints = transport.Type switch
        {
            "Walk" => 20,
            "Bike" => 10,
            "Train" => 5,
            "Bus" => 0,
            _ => 0
        };
        return basepoints + bonusPoints;
    }

    public async Task<Transport> CreateTransport(Transport transport)
    {
        transport.AwardedPoints = CalculatePoints(transport);
        _context.Transports.Add(transport);
        await _context.SaveChangesAsync();
        return transport;
    }
    public async Task<IEnumerable<Transport>> GetAllTransport()
    {
        return await _context.Transports.ToListAsync();
    }
    {
        return await _context.Transports.ToListAsync();
    }

    public async Task<Transport> GetTransportById(int id)
    {
        return await _context.Transports.FindAsync(id);
    }
    public async Task<Transport> GetTransportById(int id)
    {
        return await _context.Transports.FindAsync(id);
    }

    public async Task<Transport> RemoveTransport(int id)
    {
        var transport = await _context.Transports.FindAsync(id);
        _context.Transports.Remove(transport);
        await _context.SaveChangesAsync();
        return await Task.FromResult(transport);
    }

    {
        var transport = await _context.Transports.FindAsync(id);
        _context.Transports.Remove(transport);
        await _context.SaveChangesAsync();
        return await Task.FromResult(transport);
    }


    public async Task<Transport> UpdateTransport(Transport transport)
    {
        var transportToUpdate = await _context.Transports.FindAsync(transport.Id);
        if (transportToUpdate != null)
        {
            transportToUpdate.Name = transport.Name;
            transportToUpdate.AwardedPoints = transport.AwardedPoints;
            transportToUpdate.Date = transport.Date;
            transportToUpdate.Distance = transport.Distance;
            transportToUpdate.Type = transport.Type;
            transportToUpdate.EmissionsSaved = transport.EmissionsSaved;
        }
        await _context.SaveChangesAsync();

        return await Task.FromResult(transportToUpdate);
    }
}
    {
        var transportToUpdate = await _context.Transports.FindAsync(transport.Id);
        if (transportToUpdate != null)
        {
            transportToUpdate.Name = transport.Name;
            transportToUpdate.AwardedPoints = transport.AwardedPoints;
            transportToUpdate.Date = transport.Date;
            transportToUpdate.Distance = transport.Distance;
            transportToUpdate.Type = transport.Type;
            transportToUpdate.EmissionsSaved = transport.EmissionsSaved;
        }
        await _context.SaveChangesAsync();

        return await Task.FromResult(transportToUpdate);
    }
    public async Task<IEnumerable<Transport>> GetTransportByDatesAsync(DateOnly startDate, DateOnly endDate)
    {
        return await Task.FromResult(_context.Transports.Where(t => t.Date >= startDate && t.Date <= endDate).AsEnumerable());
    }
}