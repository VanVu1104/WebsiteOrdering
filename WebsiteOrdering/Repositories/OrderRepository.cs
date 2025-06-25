using Microsoft.EntityFrameworkCore;
using WebsiteOrdering.Models;

namespace WebsiteOrdering.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateOrderAsync(Donhang order, List<Chitietdonhang> details, List<Chitiettopping> toppings)
        {
            _context.dhang.Add(order);
            _context.ctdh.AddRange(details);
            _context.cttopping.AddRange(toppings);
            await _context.SaveChangesAsync();
            return order.Iddonhang;
        }

        public async Task<Donhang?> GetOrderWithDetailsAsync(string orderId)
        {
            return await _context.dhang.Include(o => o.Chitietdonhangs)
                    .ThenInclude(od => od.IdmonanNavigation)
                    .Include(o => o.Chitietdonhangs)
                    .ThenInclude(od => od.Idmonan2Navigation)
                    .Include(o => o.Chitietdonhangs)
                    .ThenInclude(od => od.IdsizeNavigation)
                    .Include(o => o.Chitietdonhangs).ThenInclude(od => od.IddebanhNavigation)
                .Include(o => o.Chitietdonhangs)
                    .ThenInclude(od => od.Chitiettoppings)
                        .ThenInclude(ct => ct.IdtoppingNavigation)
                .FirstOrDefaultAsync(o => o.Iddonhang == orderId);
        }

        public async Task<Donhang?> FindOrderAsync(string orderId)
        {
            return await _context.dhang.FindAsync(orderId);
        } 
        public async Task<Chitietdonhang?> FindDetailAsync(string detailsId)
        {
            return await _context.ctdh.FindAsync(detailsId);
        }
        public async Task<string?> FindDeBanhAsync(string tendebanh)
        {
            var Iddebanh = await _context.debanh
            .Where(d => d.Tendebanh == tendebanh)
            .Select(d => d.Iddebanh)
            .FirstOrDefaultAsync();
            if (Iddebanh == null)
            {
                return null;
            }
            return Iddebanh;
        }
        public async Task<string?> FindIdSizeAsync(string tenSize)
        {
            var idSize = await _context.Sizes
            .Where(d => d.Tensize == tenSize)
            .Select(d => d.Idsize)
            .FirstOrDefaultAsync();
            if (idSize == null)
            {
                return null;
            }
            return idSize;
        }
    }
}
