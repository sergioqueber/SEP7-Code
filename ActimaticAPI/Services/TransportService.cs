using Interfaces;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Storage;
using Microsoft.EntityFrameworkCore;


namespace Services;
 public class TransportService(ApplicationDbContext context)  : ITransportService
    {
        private readonly ApplicationDbContext _context = context;

        static TransportService()
        {
        }

    public async Task<Transport> CreateTransport(Transport transport)
        {
            _context.Transports.Add(transport);
            await _context.SaveChangesAsync();
            return transport;
        }
    public async Task<IEnumerable<Transport>> GetAllTransport()
        {
            return await _context.Transports.ToListAsync();
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