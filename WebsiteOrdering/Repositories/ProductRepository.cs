using Microsoft.EntityFrameworkCore;
using WebsiteOrdering.Models;

namespace WebsiteOrdering.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Monan>> GetProductsByExactNameAsync(string productName)
        {
            // Case-insensitive exact match
            var exactMatch = await _context.SanPhams
                .Where(p => p.Tenmonan.ToLower() == productName.ToLower())
                .ToListAsync();
            if (exactMatch.Any())
            {
                return exactMatch;
            }
            var trimmedMatch = await _context.SanPhams
                .Where(p => p.Tenmonan.Trim().ToLower() == productName.Trim().ToLower())
                .ToListAsync();
            return trimmedMatch;
        }
    }
}
