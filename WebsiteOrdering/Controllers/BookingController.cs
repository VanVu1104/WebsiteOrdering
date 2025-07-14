using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Claims;
using WebsiteOrdering.Models;
using WebsiteOrdering.Repositories;

namespace WebsiteOrdering.Controllers
{
    public class BookingController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly AppDbContext _appDbContext;
        public BookingController(AppDbContext context, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _appDbContext = context;
        }
        //Tạo mã iddondatban
        private static string GenerateRandomId(int length = 5)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
       
        public IActionResult Index()
        {
            ViewBag.ChiNhanh = _appDbContext.chinhanh.ToList();
            ViewBag.Ban = _appDbContext.bans.ToList();
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = _appDbContext.Users.FirstOrDefault(u => u.Id == userId);
                ViewBag.UserInfo = user;
            }

            return View(new Datban());
        }
        //Lấy danh sách khu vực theo chi nhánh
        [HttpGet]
        public IActionResult GetKhuvucByChinhanh(string idChinhanh)
        {
            var khuvucs = _appDbContext.bans
                .Where(b => b.Idchinhanh == idChinhanh)
                .Select(b => b.Khuvuc)
                .Distinct()
                .ToList();

            return Json(khuvucs);
        }
        //Lấy danh sách bàn theo khu vực
        [HttpGet]
        public IActionResult GetBanByKhuvuc(string idChinhanh, string khuvuc)
        {
            var bans = _appDbContext.bans
                .Where(b => b.Idchinhanh == idChinhanh && b.Khuvuc == khuvuc)
                .Select(b => new { b.Idban, b.Tenban,b.Songuoi,b.X,b.Y })
                .ToList();

            return Json(bans);
        }

        //Hàm đặt bàn 
        [HttpPost]
        public async Task<IActionResult> DatBan(Datban datban, ApplicationUser user, string selectedIdban)
        {
           
            string? idNguoiDung = null;
            string tenNguoiDat = datban.Tenngdat;
            string sdtNguoiDat = datban.Sđtngdat;

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                // Đã đăng nhập lấy user hiện tại
                idNguoiDung = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var currentUser = await _accountRepository.GetUserByIdAsync(idNguoiDung);

                // Nếu người dùng không sửa thông tin  gán mặc định từ user
                if (string.IsNullOrWhiteSpace(tenNguoiDat))
                {
                    tenNguoiDat = currentUser?.FullName ?? currentUser?.UserName ?? currentUser?.Email ?? "";
                }

                if (string.IsNullOrWhiteSpace(sdtNguoiDat) && !string.IsNullOrEmpty(currentUser?.PhoneNumber))
                {
                    sdtNguoiDat = currentUser.PhoneNumber;
                }
            }
            else
            {
                // Chưa đăng nhập kiểm tra user theo email
                var existingUser = await _accountRepository.GetUserByEmailAsync(user.Email);

                if (existingUser != null)
                {
                    idNguoiDung = existingUser.Id;
                }
                else
                {
                    // Tạo user mới giả lập
                    var newUser = new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = user.Email,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        FullName = user.FullName,
                        EmailConfirmed = false
                    };

                    _appDbContext.Users.Add(newUser);
                    await _appDbContext.SaveChangesAsync();

                    idNguoiDung = newUser.Id;
                }

                // Gán tên và SĐT từ form (user.FullName & PhoneNumber)
                if (string.IsNullOrWhiteSpace(tenNguoiDat))
                {
                    tenNguoiDat = user.FullName ?? "";
                }

                if (string.IsNullOrWhiteSpace(sdtNguoiDat) && !string.IsNullOrEmpty(user.PhoneNumber))
                {
                    sdtNguoiDat = user.PhoneNumber;
                }

            }

            // Tự động cộng 2 giờ
            var gioKetThuc = datban.Giobatdau.Add(TimeSpan.FromHours(2));

            // Kiểm tra bàn đã được đặt trong khoảng thời gian đó chưa
            var isBanDaDat = await _appDbContext.chitietdatbans
                .Include(c => c.IddatbanNavigation)
                .AnyAsync(c => c.Idban == selectedIdban
                    && c.IddatbanNavigation.Ngaydat == datban.Ngaydat
                      && c.IddatbanNavigation.Trangthaidatban != "Đã hủy"
                    && (
                        (datban.Giobatdau >= c.Giovao && datban.Giobatdau < c.Giora) ||     // giao nhau
                        (gioKetThuc > c.Giovao && gioKetThuc <= c.Giora) ||
                        (datban.Giobatdau <= c.Giovao && gioKetThuc >= c.Giora)            // bao phủ toàn bộ
                    )
                );

            if (isBanDaDat)
            {
                TempData["Error"] = "Bàn đã được đặt trong khung giờ này!";
                ViewBag.ChiNhanh = _appDbContext.chinhanh.ToList();
                return View(datban);
            }
            // Lấy thông tin bàn
            var ban = await _appDbContext.bans.FirstOrDefaultAsync(b => b.Idban == selectedIdban);
            if (ban == null)
            {
                TempData["Error"] = "Không tìm thấy bàn đã chọn!";
                return View(datban);
            }

            // So sánh số người đặt với sức chứa
            if (datban.Songuoidat > ban.Songuoi)
            {
                TempData["Error"] = $"Bàn chỉ chứa tối đa {ban.Songuoi} người. Vui lòng chọn bàn khác hoặc giảm số lượng.";
                ViewBag.ChiNhanh = _appDbContext.chinhanh.ToList();
                return View(datban);
            }

            var datBan = new Datban
            {
                Iddatban = GenerateRandomId(),
                Ngaydat = datban.Ngaydat,
                Giobatdau = datban.Giobatdau,
                Gioketthuc = gioKetThuc,
                Songuoidat = datban.Songuoidat,
                Ghichu = datban.Ghichu ?? "",
                Trangthaidatban = "Chờ xác nhận",
                Idngdung = idNguoiDung,
                Idchinhanh = datban.Idchinhanh,
                Tenngdat = tenNguoiDat,
                Sđtngdat = sdtNguoiDat
            };
            _appDbContext.Datbans.Add(datBan);

            var chitiet = new Chitietdatban
            {
                Iddatban = datBan.Iddatban,
                Idban = selectedIdban,
                Giovao = datban.Giobatdau,  // ban đầu là giờ khách chọn, sau có thể cập nhật nếu đến trễ
                Giora = gioKetThuc
            };
            _appDbContext.chitietdatbans.Add(chitiet);

            //if (ban != null)
            //{
            //    ban.Trangthaiban = "Đã đặt";
            //}

            await _appDbContext.SaveChangesAsync();

            TempData["Success"] = "Đặt bàn thành công!";
            return RedirectToAction("ChitietDatBan", new { id = datBan.Iddatban });
        }
        //Lấy danh sách bàn đã đặt theo ngày và giờ vào, giờ ra theo chi nhánh , khu vực
        [HttpGet]
        public IActionResult GetBanDaDat(string ngay, string gio, string idChinhanh, string idKhuvuc)
        {
            //Dùng TimeOnly.Parse thay vì TimeSpan
            var gioBatDau = TimeOnly.Parse(gio);
            var gioKetThuc = gioBatDau.Add(TimeSpan.FromHours(2));

            var danhSachBan = _appDbContext.chitietdatbans
                .Include(c => c.IddatbanNavigation)
                .Include(c => c.IdbanNavigation) // ⚠️ Bổ sung Include để dùng c.IdbanNavigation.Khuvuc
                .Where(c =>
                    c.IddatbanNavigation.Ngaydat == DateOnly.Parse(ngay) &&
                    c.IddatbanNavigation.Idchinhanh == idChinhanh &&
                    c.IdbanNavigation.Khuvuc == idKhuvuc &&
                    c.IddatbanNavigation.Trangthaidatban != "Đã hủy" &&
                    (
                        (gioBatDau >= c.Giovao && gioBatDau < c.Giora) ||
                        (gioKetThuc > c.Giovao && gioKetThuc <= c.Giora) ||
                        (gioBatDau <= c.Giovao && gioKetThuc >= c.Giora)
                    )
                )
                .Select(c => new
                {
                    idban = c.IdbanNavigation.Idban,
                    ngay = c.IddatbanNavigation.Ngaydat.ToString("yyyy-MM-dd"),
                    gio = c.Giovao.ToString(@"HH\:mm"),
                    idchinhanh = c.IddatbanNavigation.Idchinhanh,
                    idkhuvuc = c.IdbanNavigation.Khuvuc
                })
                .ToList();

            return Json(danhSachBan);
        }


        //Hiển thị chi tiết đơn đặt bàn theo id
        public async Task<IActionResult> ChitietDatBan(string id)
        {
            var datBan = await _appDbContext.Datbans
                .Include(d=>d.Nguoidung)
                .Include(d =>d.IdchinhanhNavigation)
                .Include(d => d.Chitietdatbans)
                   .ThenInclude(ct => ct.IdbanNavigation)
        .FirstOrDefaultAsync(d => d.Iddatban == id);

            if (datBan == null)
            {
                return NotFound("Không tìm thấy đơn đặt bàn.");
            }

            try
            {
                return View("ChitietDatBan", datBan);
            }
            catch (Exception ex)
            {
                return Content("Lỗi hiển thị view: " + ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> SearchDonDatBan(string maDon, string sdt, string tenNguoiDat,string emailNguoiDat)
        {
            // Check nếu không nhập gì hết thì báo lỗi
            if (string.IsNullOrWhiteSpace(maDon) && string.IsNullOrWhiteSpace(sdt) && string.IsNullOrWhiteSpace(tenNguoiDat) &&string.IsNullOrWhiteSpace(emailNguoiDat))
            {
                ViewBag.Message = "Vui lòng nhập ít nhất 1 trường để tìm kiếm.";
                return View(new List<Datban>());
            }

            var query = _appDbContext.Datbans
                .Include(d => d.Nguoidung)
                .Include(d => d.IdchinhanhNavigation)
                .Include(d => d.Chitietdatbans)
                    .ThenInclude(ct => ct.IdbanNavigation)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(maDon))
                query = query.Where(d => d.Iddatban != null && d.Iddatban.Trim().ToLower() == maDon.Trim().ToLower());

            if (!string.IsNullOrWhiteSpace(sdt))
                query = query.Where(d => d.Sđtngdat != null && d.Sđtngdat.Contains(sdt.Trim()));

            if (!string.IsNullOrWhiteSpace(tenNguoiDat))
                query = query.Where(d => d.Tenngdat != null && d.Tenngdat.Contains(tenNguoiDat.Trim()));
            //if (!string.IsNullOrWhiteSpace(emailNguoiDat))
            //    query = query.Where(d => d.Nguoidung != null && d.Nguoidung.Email != null && d.Nguoidung.Email.Contains(emailNguoiDat.Trim()));

            if (!string.IsNullOrWhiteSpace(emailNguoiDat))
            {
                var email = emailNguoiDat.Trim();

                // Kiểm tra xem chuỗi nhập có chứa @gmail.com không
                if (!email.Contains("@gmail.com", StringComparison.OrdinalIgnoreCase))
                {
                    ViewBag.Message = "Vui lòng nhập đúng định dạng email (ví dụ: example@gmail.com).";
                    return View(new List<Datban>());
                }

                query = query.Where(d => d.Nguoidung != null && d.Nguoidung.Email != null && d.Nguoidung.Email.Contains(email));
            }


            var result = await query.OrderByDescending(d => d.Ngaydat).ToListAsync();

            return View(result);
        }


    }
}
