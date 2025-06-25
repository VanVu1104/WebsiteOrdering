using Microsoft.AspNetCore.Mvc;
using WebsiteOrdering.ViewModels;
using WebsiteOrdering.Helper;
using WebsiteOrdering.Repositories;
using WebsiteOrdering.Services;
using System.Security.Claims;

namespace WebsiteOrdering.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;
        private readonly IOrderRepository _orderRepository;
        public CheckoutController(IOrderRepository orderRepository, ICheckoutService checkoutService)
        {
            _orderRepository = orderRepository;
            _checkoutService = checkoutService;
        }
        [HttpPost]
        public async Task<IActionResult> Confirm(UserCheckoutInfoViewModel userInfo, [FromForm] List<string> selectedIds)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new();
            var selectedItems = _checkoutService.GetSelectedItems(cart, selectedIds);

            if (!selectedItems.Any())
            {
                TempData["Error"] = "Không có món nào được chọn.";
                return RedirectToAction("Index", "Cart");
            }

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Vui lòng điền đầy đủ thông tin đặt hàng.";
                return RedirectToAction("Index", "Cart");
            }

            var userId = User.Identity?.IsAuthenticated == true ? User.FindFirst(ClaimTypes.NameIdentifier)?.Value : null;
            var orderId = await _checkoutService.CreateOrderAsync(selectedItems, userInfo, userId);

            // Xóa sản phẩm đã đặt khỏi giỏ hàng
            var updatedCart = cart.Except(selectedItems).ToList();
            HttpContext.Session.Set("Cart", updatedCart);

            return RedirectToAction("Success", new { id = orderId });
        }
        [HttpGet]
        public async Task<IActionResult> Success(string id)
        {
            var order = await _orderRepository.GetOrderWithDetailsAsync(id);
            if (order == null) return NotFound("Không tìm thấy đơn hàng.");
            return View(order);
        }
        [HttpGet]
        public async Task<IActionResult> CheckOrderStatus(string orderId)
        {
            var order = await _orderRepository.FindOrderAsync(orderId);
            if (order == null)
            {
                return Json(new { success = false, message = "Không tìm thấy đơn hàng" });
            }

            return Json(new
            {
                success = true,
                status = order.Trangthai,
                orderDate = order.Ngaydat.ToString("dd/MM/yyyy HH:mm"),
                totalAmount = order.Tongtien
            });
        }
    }
}
