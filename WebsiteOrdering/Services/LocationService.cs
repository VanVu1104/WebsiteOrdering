using System.Net.Http;
using System.Text;
using System.Text.Json;
using WebsiteOrdering.Models;
using WebsiteOrdering.Repositories;
using WebsiteOrdering.ViewModels;

namespace WebsiteOrdering.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            var locations = await _locationRepository.GetAllAsync();
            return locations.Select(MapToViewModel).ToList();
        }

        public async Task<Location?> GetLocationByIdAsync(int id)
        {
            var location = await _locationRepository.GetByIdAsync(id);
            return location != null ? MapToViewModel(location) : null;
        }

        public async Task<Location> CreateLocationAsync(Location createDto)
        {
            var location = new Location
            {
                Name = createDto.Name,
                Address = createDto.Address,
                Latitude = createDto.Latitude,
                Longitude = createDto.Longitude
            };

            var createdLocation = await _locationRepository.CreateAsync(location);
            return MapToViewModel(createdLocation);
        }

        public async Task<Location> UpdateLocationAsync(int id, Location updateDto)
        {
            var existingLocation = await _locationRepository.GetByIdAsync(id);
            if (existingLocation == null)
                throw new ArgumentException($"Location with ID {id} not found.");

            existingLocation.Name = updateDto.Name;
            existingLocation.Address = updateDto.Address;
            existingLocation.Latitude = updateDto.Latitude;
            existingLocation.Longitude = updateDto.Longitude;

            var updatedLocation = await _locationRepository.UpdateAsync(existingLocation);
            return MapToViewModel(updatedLocation);
        }

        public async Task<bool> DeleteLocationAsync(int id)
        {
            return await _locationRepository.DeleteAsync(id);
        }

        public async Task<bool> LocationExistsAsync(int id)
        {
            return await _locationRepository.ExistsAsync(id);
        }

        public async Task<IEnumerable<Location>> GetLocationsByAreaAsync(decimal minLat, decimal maxLat, decimal minLng, decimal maxLng)
        {
            var locations = await _locationRepository.GetByAreaAsync(minLat, maxLat, minLng, maxLng);
            return locations.Select(MapToViewModel).ToList();
        }

        public async Task<IEnumerable<Location>> SearchLocationsByNameAsync(string name)
        {
            var locations = await _locationRepository.SearchByNameAsync(name);
            return locations.Select(MapToViewModel).ToList();
        }

        private static Location MapToViewModel(Location location)
        {
            return new Location
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
        }
        // Enhanced distance calculation (Haversine formula)
        public double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            const double R = 6371; // Earth's radius in kilometers
            var dLat = ToRadians(lat2 - lat1);
            var dLng = ToRadians(lng2 - lng1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLng / 2) * Math.Sin(dLng / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }
        private double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
        
        // Create straight line route as fallback
        private object CreateStraightLineRoute(RouteRequest request)
        {
            var distance = GetDistance(request.StartLat, request.StartLng, request.EndLat, request.EndLng);
            var estimatedDuration = (distance * 60) / 50; // Assume 50 km/h average speed

            return new
            {
                type = "FeatureCollection",
                features = new[]
                {
            new
            {
                type = "Feature",
                geometry = new
                {
                    type = "LineString",
                    coordinates = new[]
                    {
                        new[] { request.StartLng, request.StartLat },
                        new[] { request.EndLng, request.EndLat }
                    }
                },
                properties = new
                {
                    segments = new[]
                    {
                        new
                        {
                            distance = distance * 1000, // Convert to meters
                            duration = estimatedDuration * 60, // Convert to seconds
                            steps = new[]
                            {
                                new
                                {
                                    instruction = $"Đi thẳng {distance:F2} km đến đích",
                                    distance = distance * 1000,
                                    duration = estimatedDuration * 60
                                }
                            }
                        }
                    }
                }
            }
        }
            };
        }
    }
}
