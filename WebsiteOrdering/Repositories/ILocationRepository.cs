using WebsiteOrdering.Models;

namespace WebsiteOrdering.Repositories
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllAsync();
        Task<Location?> GetByIdAsync(int id);
        Task<Location> CreateAsync(Location location);
        Task<Location> UpdateAsync(Location location);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Location>> GetByAreaAsync(decimal minLat, decimal maxLat, decimal minLng, decimal maxLng);
        Task<IEnumerable<Location>> SearchByNameAsync(string name);
    }
}
