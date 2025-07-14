using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteOrdering.Helper;
using WebsiteOrdering.Models;
using WebsiteOrdering.Services;

namespace WebsiteOrdering.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Route("[area]/[controller]/[action]")]
    public class BookingStaffController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IEmailService _emailService;
        public BookingStaffController(AppDbContext context, IEmailService emailService)
        {
            _emailService = emailService;
            _appDbContext = context;
        }
        public  async Task<IActionResult> Index(string idChiNhanh = null, string trangThai = null, DateTime? tuNgay = null)
        {
            ViewBag.ChiNhanhList = await _appDbContext.chinhanh.ToListAsync();

            var query = _appDbContext.Datbans
                .Include(d => d.IdchinhanhNavigation)
                .Include(d => d.Nguoidung)
                .Include(d => d.Chitietdatbans)
                    .ThenInclude(ct => ct.IdbanNavigation)
            .AsQueryable();

            var baseFilter = _appDbContext.Datbans.AsQueryable();

            var idChiNhanhNhanVien = User.FindFirst("ChiNhanhId")?.Value;

            // Bắt buộc phải lọc theo chi nhánh nhân viên nếu có
            if (!string.IsNullOrEmpty(idChiNhanhNhanVien))
            {
                query = query.Where(d => d.Idchinhanh == idChiNhanhNhanVien);
                baseFilter = baseFilter.Where(d => d.Idchinhanh == idChiNhanhNhanVien);
                // Gán luôn idChiNhanh để giữ lại hiển thị filter
                idChiNhanh = idChiNhanhNhanVien;
            }
            else
            {
                // Nếu vẫn muốn cho admin chọn filter chi nhánh thủ công, giữ code cũ
                if (!string.IsNullOrEmpty(idChiNhanh))
                {
                    query = query.Where(d => d.Idchinhanh == idChiNhanh);
                    baseFilter = baseFilter.Where(d => d.Idchinhanh == idChiNhanh);
                }
            }

            //// Lọc theo ngày nếu có
            //if (tuNgay.HasValue)
            //{
            //    var tuNgayOnly = DateOnly.FromDateTime(tuNgay.Value);
            //    query = query.Where(d => d.Ngaydat == tuNgayOnly);
            //    baseFilter = baseFilter.Where(d => d.Ngaydat == tuNgayOnly);
            //}

            //// Lọc trạng thái nếu có
            //if (!string.IsNullOrEmpty(trangThai))
            //{
            //    query = query.Where(d => d.Trangthaidatban == trangThai);
            //}

            // Đếm đơn theo trạng thái (dùng baseFilter)
            ViewBag.CountChoXacNhan = await baseFilter.CountAsync(d => d.Trangthaidatban == "Chờ xác nhận");
            ViewBag.CountDaXacNhan = await baseFilter.CountAsync(d => d.Trangthaidatban == "Đã xác nhận");
            ViewBag.CountDaHuy = await baseFilter.CountAsync(d => d.Trangthaidatban == "Đã hủy");

            var result = await query.OrderBy(d => d.Ngaydat).ToListAsync();

            // Truyền lại giá trị lọc
            ViewBag.SelectedChiNhanh = idChiNhanh;
            ViewBag.SelectedTrangThai = trangThai;
            ViewBag.TuNgay = tuNgay?.ToString("yyyy-MM-dd");

            return View(result);
        }
        public async Task<IActionResult> DetailDonDatBan(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            id = id.Trim();

            var datban = await _appDbContext.Datbans
              .Include(d => d.Nguoidung)
              .Include(d => d.IdchinhanhNavigation)
              .Include(d => d.Chitietdatbans)
                  .ThenInclude(ct => ct.IdbanNavigation)
              .ToListAsync(); // await được vì trả về Task<List<>>

            var foundDatban = datban
                .FirstOrDefault(d => d.Iddatban?.Trim().Equals(id, StringComparison.OrdinalIgnoreCase) == true);

            if (foundDatban == null)
            {
                return NotFound();
            }

            return View("DetailDonDatBan", foundDatban);

        }

        [HttpPost]
        public async Task<IActionResult> XacNhanDatBan(string id)
        {
            var datban = await _appDbContext.Datbans
                .Include(d => d.Nguoidung)
                .Include(d => d.IdchinhanhNavigation)
                .FirstOrDefaultAsync(d => d.Iddatban == id);

            if (datban == null) return NotFound();

            datban.Trangthaidatban = "Đã xác nhận";
            await _appDbContext.SaveChangesAsync();

            var email = datban.Nguoidung?.Email;
            if (!string.IsNullOrEmpty(email))
            {
                var placeholders = new Dictionary<string, string>
                {
                    ["TenNguoiDat"] = datban.Tenngdat ?? datban.Nguoidung?.FullName ?? "Khách hàng",
                    ["TenChiNhanh"] = datban.IdchinhanhNavigation.Tencnhanh,
                    ["NgayDat"] = datban.Ngaydat.ToString("dd/MM/yyyy"),
                    ["GioBatDau"] = datban.Giobatdau.ToString(),
                    ["GioKetThuc"] = datban.Gioketthuc.ToString()
                };

                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "EmailXacNhanDatBan.html");

                var body = EmailTemplateHelper.PopulateTemplate(templatePath, placeholders);

                await _emailService.SendEmailAsync(email, "Xác nhận đặt bàn thành công", body);
            }

            return RedirectToAction("Index", new { idChiNhanh = datban.Idchinhanh, trangThai = "Chờ xác nhận" });
        }
        [HttpPost]
        public async Task<IActionResult> HuyDatBan(string id, string lyDo, string lyDoChiTiet)
        {
            var datban = await _appDbContext.Datbans
            .Include(d => d.Nguoidung)
            .Include(d => d.IdchinhanhNavigation)
            .FirstOrDefaultAsync(d => d.Iddatban == id);
            if (datban == null) return NotFound();

            datban.Trangthaidatban = "Đã hủy";
            // Nếu chọn "Khác" thì lưu lý do chi tiết
            if (lyDo == "Khác" && !string.IsNullOrWhiteSpace(lyDoChiTiet))
            {
                datban.Lydo = lyDoChiTiet;
            }
            else
            {
                datban.Lydo = lyDo;
            }
            _appDbContext.Entry(datban).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            var email = datban.Nguoidung?.Email;
            if (!string.IsNullOrEmpty(email))
            {
                var placeholders = new Dictionary<string, string>
                {
                    ["TenNguoiDat"] = datban.Tenngdat ?? datban.Nguoidung?.FullName ?? "Khách hàng",
                    ["TenChiNhanh"] = datban.IdchinhanhNavigation.Tencnhanh,
                    ["NgayDat"] = datban.Ngaydat.ToString("dd/MM/yyyy"),
                    ["GioBatDau"] = datban.Giobatdau.ToString(),
                    ["GioKetThuc"] = datban.Gioketthuc.ToString(),
                    ["LyDo"] = datban.Lydo.ToString()
                };

                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "EmailHuyDatBan.html");

                var body = EmailTemplateHelper.PopulateTemplate(templatePath, placeholders);

                await _emailService.SendEmailAsync(email, "Xác nhận hủy đặt bàn thành công", body);
            }

            return RedirectToAction("Index", new { idChiNhanh = datban.Idchinhanh, trangThai = "Đã hủy" });
        }

    }
}
