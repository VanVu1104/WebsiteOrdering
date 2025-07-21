using WebsiteOrdering.Areas.ViewModelAdmin;
using WebsiteOrdering.Models;

namespace WebsiteOrdering.Areas.Services
{
    public interface IOrderService
    {
        List<string> GetAvailableStatuses(string currentStatus);
        Task<(List<Donhang> Orders, int TotalCount)> GetPagedFilteredOrdersAsync(OrderFilterModel filter);
        (DateTime startDate, DateTime endDate) GetDateRange(string dateFilter, DateTime? fromDate, DateTime? toDate);

    }
}
