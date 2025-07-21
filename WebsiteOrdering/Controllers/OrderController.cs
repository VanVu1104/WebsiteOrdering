using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteOrdering.Models;
using WebsiteOrdering.Repositories;

namespace WebsiteOrdering.Controllers
{
    public class OrderController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly AppDbContext _context;
        private readonly IOrderRepository _orderRepository;

        public OrderController(IAccountRepository accountRepository, AppDbContext context, 
            IOrderRepository orderRepository)
        {
            _accountRepository = accountRepository;
            _context = context;
            _orderRepository = orderRepository;
        }
        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View(new List<Donhang>());
            }

            // Lấy user đang đăng nhập
            var user = await _accountRepository.GetCurrentUserAsync(User);
            if (user == null)
            {
                return View(new List<Donhang>());
            }

            // Truy vấn đơn hàng theo Id người dùng
            var orders = await _orderRepository.GetOrdersByUserIdAsync(user.Id);
            return View(orders);
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var order = await _orderRepository.GetOrderWithDetailsAsync(id);
            if (order == null)
            {
                return NotFound("Không tìm thấy đơn hàng.");
            }
            return View(order);
        }
    }
}
