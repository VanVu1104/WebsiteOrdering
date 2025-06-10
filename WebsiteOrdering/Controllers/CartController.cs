using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteOrdering.Helper;
using WebsiteOrdering.Models;
using WebsiteOrdering.ViewModels;

namespace WebsiteOrdering.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public CartController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Thêm vào giỏ hàng
        //[HttpPost]
        //public async Task<IActionResult> AddToCart(string idmonan, string idsize,string iddebanh,List<string> toppings, int soluong,string ghichu,string idmonan2)
        //{
        //    var product = await _appDbContext.SanPhams
        //        .Where(p => p.IDMONAN == idmonan)
        //        .Select(p => new
        //        {
        //            p.IDMONAN,
        //            p.IDMONAN2,
        //            p.TENMONAN,
        //            p.ANHMONAN,
        //            p.GIACOBAN,
        //            p.IDLoaiMonAn
        //        })
        //        .FirstOrDefaultAsync();

        //    var size = await _appDbContext.Sizes.FindAsync(idsize);
        //    var debanh = await _appDbContext.debanh.FindAsync(iddebanh);

        // //  string idmonan2 = product.IDMONAN2;

        //    int giacoban = product.GIACOBAN;
        //    int giasize = _appDbContext.ListGiaSizes.FirstOrDefault(l => l.IDLOAIMONAN == product.IDLoaiMonAn && l.IDSIZE == idsize)?.GIA ?? 0;
        //    int giadebanh = debanh?.GIADEBANH ?? 0;

        //    var toppingObjs = new List<ToppingViewModel>();
        //    if (toppings != null && toppings.Count > 0)
        //    {
        //        toppingObjs = await _appDbContext.Topping
        //            .Where(t=> toppings.Contains(t.IDTOPPING))
        //            .Select(t=> new ToppingViewModel
        //            {
        //                IDTOPPING = t.IDTOPPING,
        //                TENTOPPING = t.TENTOPPING,
        //                GIATOPPING = t.GIATOPPING
        //            }).ToListAsync();
        //    }
        //    //var cart = HttpContext.Session.Get<List<CartItem>>("Cart");
        //    var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();

        //    // Kiểm tra trùng sản phẩm
        //    var existingItem = cart.FirstOrDefault(c =>
        //        c.IDMONAN == idmonan &&
        //        c.Size == size.TENSIZE &&
        //        c.DeBanh == debanh.TENDEBANH &&
        //        c.Topping.Select(t => t.IDTOPPING).OrderBy(x => x).SequenceEqual(toppingObjs.Select(t => t.IDTOPPING).OrderBy(x => x)));

        //    if (existingItem != null)
        //    {
        //        existingItem.SoLuong += soluong;
        //    }
        //    else
        //    {
        //        cart.Add(new CartItem
        //        {
        //            IDMONAN = product.IDMONAN,
        //            IDMONAN2 = product.IDMONAN2,
        //            TENSANPHAM = product.TENMONAN,
        //            ANHSANPHAM = product.ANHMONAN,
        //            Size = size.TENSIZE,
        //            DeBanh = debanh.TENDEBANH,
        //            SoLuong = soluong,
        //            GhiChu = ghichu,
        //            GiaCoBan = giacoban,
        //            GiaSize = giasize,
        //            GiaDeBanh = giadebanh,
        //            Topping = toppingObjs
        //        });
        //    }

        //    // Lưu lại vào session
        //    HttpContext.Session.Set("Cart", cart);

        //    return RedirectToAction("Index", "Cart");

        //}

  
        [HttpPost]
        public async Task<IActionResult> AddToCart(IFormCollection form)
        {
            // Debug: In ra tất cả dữ liệu từ form
            foreach (var key in form.Keys)
            {
                Console.WriteLine($"{key}: {form[key]}");
            }

            // Lấy dữ liệu từ form
            string idmonan = form["idmonan"];
            string idsize = form["SelectedSizeId"];
            string iddebanh = form["SelectedDeBanhId"];
            string ghichu = form["ghichu"];
            string idmonan2 = form["idmonan2"];

            // Parse các giá trị số
            int soluong = 1;
            if (form.ContainsKey("soluong") && !string.IsNullOrEmpty(form["soluong"]))
            {
                int.TryParse(form["soluong"], out soluong);
            }

            // Lấy danh sách toppings
            List<string> toppings = new List<string>();
            if (form.ContainsKey("SelectedToppingIds"))
            {
                toppings = form["SelectedToppingIds"].ToList();
            }

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(idmonan))
            {
                return BadRequest("ID món ăn không hợp lệ");
            }

            // Tìm sản phẩm
            //var product = await _appDbContext.SanPhams
            //    .Where(p => p.IDMONAN == idmonan)
            //    .Select(p => new
            //    {
            //        p.IDMONAN,
            //        p.IDMONAN2,
            //        p.TENMONAN,
            //        p.ANHMONAN,
            //        p.GIACOBAN,
            //        p.IDLoaiMonAn
            //    })
            //    .FirstOrDefaultAsync();
            var product = await _appDbContext.SanPhams
                .Include(p=>p.Category)
    .Where(p => p.IDMONAN == idmonan).FirstOrDefaultAsync();

           
            // Kiểm tra product có tồn tại không
            if (product == null)
            {
                return BadRequest("Sản phẩm không tồn tại");
            }

            // Tìm size và đế bánh
            var size = await _appDbContext.Sizes.FindAsync(idsize);
            var debanh = await _appDbContext.debanh.FindAsync(iddebanh);

            // Kiểm tra size và debanh có tồn tại không
            if (size == null || debanh == null)
            {
                return BadRequest("Size hoặc đế bánh không hợp lệ");
            }

            // Tính giá
            int giacoban = product.GIACOBAN;
            int giasize = _appDbContext.ListGiaSizes
                .FirstOrDefault(l => l.IDLOAIMONAN == product.IDLoaiMonAn && l.IDSIZE == idsize)?.GIA ?? 0;
            int giadebanh = debanh.GIADEBANH;

            // Lấy thông tin toppings
            var toppingObjs = new List<ToppingViewModel>();
            if (toppings != null && toppings.Count > 0)
            {
                toppingObjs = await _appDbContext.Topping
                    .Where(t => toppings.Contains(t.IDTOPPING))
                    .Select(t => new ToppingViewModel
                    {
                        IDTOPPING = t.IDTOPPING,
                        TENTOPPING = t.TENTOPPING,
                        GIATOPPING = t.GIATOPPING
                    }).ToListAsync();
            }

            // Lấy giỏ hàng từ session
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Kiểm tra sản phẩm đã tồn tại trong giỏ hàng chưa
            var existingItem = cart.FirstOrDefault(c =>
                c.IDMONAN == idmonan &&
                c.Size == size.TENSIZE &&
                c.DeBanh == debanh.TENDEBANH &&
                c.Topping.Select(t => t.IDTOPPING).OrderBy(x => x)
                    .SequenceEqual(toppingObjs.Select(t => t.IDTOPPING).OrderBy(x => x)));

            if (existingItem != null)
            {
                // Nếu đã tồn tại, tăng số lượng
                existingItem.SoLuong += soluong;
            }
            else
            {
                // Nếu chưa tồn tại, thêm mới vào giỏ hàng
                cart.Add(new CartItem
                {
                    IDMONAN = product.IDMONAN,
                    IDMONAN2 = product.IDMONAN2,
                    TENSANPHAM = product.TENMONAN,
                    ANHSANPHAM = product.ANHMONAN,
                    Size = size.TENSIZE,
                    DeBanh = debanh.TENDEBANH,
                    SoLuong = soluong,
                    GhiChu = ghichu,
                    GiaCoBan = giacoban,
                    GiaSize = giasize,
                    GiaDeBanh = giadebanh,
                    Topping = toppingObjs
                });
            }

            // Lưu giỏ hàng vào session
            HttpContext.Session.Set("Cart", cart);

            return RedirectToAction("Index", "Cart");
        }
        //Hiển thị giỏ hàng
        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }

        //Cập nhật số lượng
        [HttpPut]
        public IActionResult UpdateCart(string idmonan, string size, string debanh, List<string> toppings, int soluong ,string idmonan2)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();

            var item = cart.FirstOrDefault(c =>
                c.IDMONAN == idmonan &&
                c.IDMONAN2 == idmonan2 &&
                c.Size == size &&
                c.DeBanh == debanh &&
                c.Topping.Select(t => t.IDTOPPING).OrderBy(x => x).SequenceEqual(toppings.OrderBy(x => x)));

            if (item != null)
            {
                item.SoLuong = soluong;
            }

            HttpContext.Session.Set("Cart", cart);
            return RedirectToAction("Index");
        }
        //Xóa sản phẩm ra giỏ hàng
        [HttpDelete]
        public IActionResult DeleteItem(string idmonan, string size, string debanh, List<string> toppings)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();

            var itemToRemove = cart.FirstOrDefault(c =>
                c.IDMONAN == idmonan &&
                c.Size == size &&
                c.DeBanh == debanh &&
                c.Topping.Select(t => t.IDTOPPING).OrderBy(x => x).SequenceEqual(toppings.OrderBy(x => x)));

            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
            }

            HttpContext.Session.Set("Cart", cart);
            return RedirectToAction("Index");
        }

        // DELETE: Xoá toàn bộ giỏ hàng
        [HttpDelete]
        public IActionResult CartEmpty()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }

    }
}
