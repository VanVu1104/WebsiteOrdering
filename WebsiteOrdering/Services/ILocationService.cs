using WebsiteOrdering.Models;

namespace WebsiteOrdering.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAllLocationsAsync();
        Task<Location?> GetLocationByIdAsync(int id);
        Task<Location> CreateLocationAsync(Location createDto);
        Task<Location> UpdateLocationAsync(int id, Location updateDto);
        Task<bool> DeleteLocationAsync(int id);
        Task<bool> LocationExistsAsync(int id);
        Task<IEnumerable<Location>> GetLocationsByAreaAsync(decimal minLat, decimal maxLat, decimal minLng, decimal maxLng);
        Task<IEnumerable<Location>> SearchLocationsByNameAsync(string name);
        double GetDistance(double lat1, double lng1, double lat2, double lng2);

    }
}
