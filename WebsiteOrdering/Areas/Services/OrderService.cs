using WebsiteOrdering.Areas.ViewModelAdmin;
using WebsiteOrdering.Models;
using WebsiteOrdering.Repositories;

namespace WebsiteOrdering.Areas.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<string> GetAvailableStatuses(string currentStatus)
        {
            return currentStatus switch
            {
                "Pending" => ["Pending", "Paid", "Delivering", "Delivered", "Cancel"],
                "Paid" => ["Paid", "Delivering", "Delivered", "Cancel"],
                "Delivering" => ["Delivering", "Delivered"],
                "Delivered" => ["Delivered"],
                "Cancel" => ["Cancel"],
                _ => []
            };
        }
        public async Task<(List<Donhang> Orders, int TotalCount)> GetPagedFilteredOrdersAsync(OrderFilterModel filter)
        {
            var orders = await GetOrdersByStatusAsync(filter.Status);

            if (!string.IsNullOrEmpty(filter.ChiNhanhId))
            {
                orders = orders.Where(o => o.Idchinhanh == filter.ChiNhanhId).ToList();
            }

            if (!string.IsNullOrEmpty(filter.Keyword))
            {
                orders = FilterByKeyword(orders, filter.Keyword);
            }

            orders = FilterByDateRange(orders, filter.DateFilter, filter.FromDate, filter.ToDate);

            int totalCount = orders.Count;

            orders = orders
                .OrderByDescending(o => o.Ngaydat)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            return (orders, totalCount);
        }
        public (DateTime startDate, DateTime endDate) GetDateRange(string dateFilter, DateTime? fromDate, DateTime? toDate)
        {
            var now = DateTime.Now;

            return dateFilter switch
            {
                "week" => GetWeekRange(now),
                "month" => GetMonthRange(now),
                "custom" when fromDate.HasValue && toDate.HasValue => (fromDate.Value.Date, toDate.Value.Date),
                _ => GetMonthRange(now) // Default to current month
            };
        }
        private async Task<List<Donhang>> GetOrdersByStatusAsync(string status)
        {
            return string.IsNullOrEmpty(status)
                ? await _orderRepository.GetAllOrdersAsync()
                : await _orderRepository.GetOrdersByStatusAsync(status);
        }
        private List<Donhang> FilterByKeyword(List<Donhang> orders, string keyword)
        {
            return orders.Where(o => o.Iddonhang.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        private List<Donhang> FilterByDateRange(List<Donhang> orders, string dateFilter, DateTime? fromDate, DateTime? toDate)
        {
            // Nếu có fromDate và toDate thì lọc theo custom range, bỏ qua dateFilter
            if (fromDate.HasValue && toDate.HasValue)
            {
                return orders.Where(o => o.Ngaydat.Date >= fromDate.Value.Date && o.Ngaydat.Date <= toDate.Value.Date).ToList();
            }

            // Nếu không có custom date thì xử lý theo dateFilter
            var (startDate, endDate) = GetDateRange(dateFilter, fromDate, toDate);
            return orders.Where(o => o.Ngaydat.Date >= startDate && o.Ngaydat.Date <= endDate).ToList();
        }

        private (DateTime startDate, DateTime endDate) GetWeekRange(DateTime now)
        {
            var startOfWeek = now.AddDays(-(int)now.DayOfWeek + (int)DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(6);
            return (startOfWeek.Date, endOfWeek.Date);
        }

        private (DateTime startDate, DateTime endDate) GetMonthRange(DateTime now)
        {
            var firstDay = new DateTime(now.Year, now.Month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);
            return (firstDay.Date, lastDay.Date);
        }
    }
}
