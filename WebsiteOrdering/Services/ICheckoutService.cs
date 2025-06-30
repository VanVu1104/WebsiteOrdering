using WebsiteOrdering.ViewModels;

namespace WebsiteOrdering.Services
{
    public interface ICheckoutService
    {
        List<CartItem> GetSelectedItems(List<CartItem> cart, List<string> selectedIds);
        decimal CalculateTotalAmount(List<CartItem> selectedItems);
        Task<string> CreateOrderAsync(List<CartItem> selectedItems, UserCheckoutInfoViewModel userInfo, string? userId);
        Task UpdateOrderPaymentStatusAsync(string orderId, string status, string transactionId);
    }
}
