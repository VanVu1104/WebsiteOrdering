using WebsiteOrdering.Models;

namespace WebsiteOrdering.Repositories
{
    public interface IOrderRepository
    {
        Task<string> CreateOrderAsync(Donhang order, List<Chitietdonhang> details, List<Chitiettopping> toppings);
        Task<Donhang?> GetOrderWithDetailsAsync(string orderId);
        Task<Donhang?> FindOrderAsync(string orderId);
        Task<string?> FindDeBanhAsync(string Tendebanh);
        Task<string?> FindIdSizeAsync(string tenSize);
        Task<Chitietdonhang?> FindDetailAsync(string detailsId);

    }
}
