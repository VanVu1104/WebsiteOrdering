using Microsoft.EntityFrameworkCore;
using WebsiteOrdering.Data;
using WebsiteOrdering.Models;

namespace WebsiteOrdering.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AppDbContext _context;

        public LocationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await _context.Locations
                .OrderBy(l => l.Name)
                .ToListAsync();
        }

        public async Task<Location?> GetByIdAsync(int id)
        {
            return await _context.Locations
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Location> CreateAsync(Location location)
        {
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task<Location> UpdateAsync(Location location)
        {
            _context.Locations.Update(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
                return false;

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Locations.AnyAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Location>> GetByAreaAsync(decimal minLat, decimal maxLat, decimal minLng, decimal maxLng)
        {
            return await _context.Locations
                .Where(l => l.Latitude >= minLat && l.Latitude <= maxLat &&
                           l.Longitude >= minLng && l.Longitude <= maxLng)
                .OrderBy(l => l.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Location>> SearchByNameAsync(string name)
        {
            return await _context.Locations
                .Where(l => l.Name.Contains(name) || l.Address.Contains(name))
                .OrderBy(l => l.Name)
                .ToListAsync();
        }
    }
}
