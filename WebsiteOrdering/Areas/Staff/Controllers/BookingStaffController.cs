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
        public async Task<IActionResult> Index(string trangThai = "", string idChiNhanh = "", string tuNgay = "")
        {
            var staffChiNhanhId = User.FindFirst("ChiNhanhId")?.Value;
            if (string.IsNullOrEmpty(staffChiNhanhId))
            {
                return RedirectToAction("Login", "Account");
            }
            var query = _appDbContext.Datbans
                .Include(d => d.Chitietdatbans)
                .ThenInclude(ct => ct.IdbanNavigation)
                .Include(d => d.IdchinhanhNavigation)
                .Where(d => d.Idchinhanh == staffChiNhanhId);
            if (!string.IsNullOrEmpty(trangThai))
            {
                query = query.Where(d => d.Trangthaidatban == trangThai);
            }

            if (!string.IsNullOrEmpty(tuNgay) && DateOnly.TryParse(tuNgay, out var ngayBatDau))
            {
                query = query.Where(d => d.Ngaydat >= ngayBatDau);
            }

            var datbans = await query.OrderByDescending(d => d.Ngaydat)
                .ThenByDescending(d => d.Giobatdau)
                .ToListAsync();

            // QUAN TRỌNG: Load danh sách bàn cho EditForm
            ViewBag.BanList = await _appDbContext.bans
                .Where(b => b.Idchinhanh == staffChiNhanhId)
                .OrderBy(b => b.Khuvuc)
                .ThenBy(b => b.Tenban)
                .ToListAsync();

            // Debug: Kiểm tra số lượng bàn
            var banCount = ViewBag.BanList?.Count ?? 0;
            ViewBag.Debug = $"Chi nhánh: {staffChiNhanhId}, Số bàn: {banCount}";

            // Count statistics
            var allDatbans = await _appDbContext.Datbans
                .Where(d => d.Idchinhanh == staffChiNhanhId)
                .ToListAsync();

            ViewBag.CountChoXacNhan = allDatbans.Count(d => d.Trangthaidatban == "Chờ xác nhận");
            ViewBag.CountDaXacNhan = allDatbans.Count(d => d.Trangthaidatban == "Đã xác nhận");
            ViewBag.CountDaHuy = allDatbans.Count(d => d.Trangthaidatban == "Đã hủy");

            ViewBag.SelectedTrangThai = trangThai;
            ViewBag.SelectedChiNhanh = idChiNhanh;
            ViewBag.TuNgay = tuNgay;

            return View(datbans);
        }

        //Chi tiết đơn đặt bàn
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
        //Xác nhận đơn đặt bàn
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
                    ["MaDonDatBan"] = datban.Iddatban,
                    ["TenNguoiDat"] = datban.Tenngdat ?? datban.Nguoidung?.FullName ?? "Khách hàng",
                    ["TenChiNhanh"] = datban.IdchinhanhNavigation.Tencnhanh,
                    ["NgayDat"] = datban.Ngaydat.ToString("dd/MM/yyyy"),
                    ["GioBatDau"] = datban.Giobatdau.ToString(),
                    ["GioKetThuc"] = datban.Gioketthuc.ToString()
                };

                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "EmailXacNhanDatBan.html");

                var body = EmailTemplateHelper.PopulateTemplate(templatePath, placeholders);

                await _emailService.SendEmailAsync(email, $"Xác nhận đặt bàn thành công - Mã đơn đặt bàn: {datban.Iddatban}", body);
            }

            return RedirectToAction("Index", new { idChiNhanh = datban.Idchinhanh, trangThai = "Chờ xác nhận" });
        }
        //Nhân viên hủy đặt bàn 
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
                    ["MaDonDatBan"] = datban.Iddatban,
                    ["TenNguoiDat"] = datban.Tenngdat ?? datban.Nguoidung?.FullName ?? "Khách hàng",
                    ["TenChiNhanh"] = datban.IdchinhanhNavigation.Tencnhanh,
                    ["NgayDat"] = datban.Ngaydat.ToString("dd/MM/yyyy"),
                    ["GioBatDau"] = datban.Giobatdau.ToString(),
                    ["GioKetThuc"] = datban.Gioketthuc.ToString(),
                    ["LyDo"] = datban.Lydo.ToString()
                };

                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "EmailHuyDatBan.html");

                var body = EmailTemplateHelper.PopulateTemplate(templatePath, placeholders);

                await _emailService.SendEmailAsync(email, $"Xác nhận hủy đặt bàn thành công - Mã đơn đặt bàn: {datban.Iddatban}", body);
            }

            return RedirectToAction("Index", new { idChiNhanh = datban.Idchinhanh, trangThai = "Đã hủy" });
        }

        [HttpPost]
        public async Task<IActionResult> KhachDaDen(string id)
        {
            var datban = await _appDbContext.Datbans
                .Include(d => d.Chitietdatbans)
                .FirstOrDefaultAsync(d => d.Iddatban == id);

            if (datban == null)
                return NotFound();

            var today = DateOnly.FromDateTime(DateTime.Today);
            if (datban.Ngaydat != today)
            {
                TempData["Error"] = "Chỉ có thể xác nhận khách đã đến trong ngày đặt bàn.";
                return RedirectToAction("DanhSachKhachDaDen");
            }

            // Cập nhật trạng thái
            datban.Trangthaidatban = "Khách đã đến";

            // Giờ vào (lấy giờ hiện tại)
            var gioVao = TimeOnly.FromDateTime(DateTime.Now);

            foreach (var chitiet in datban.Chitietdatbans)
            {
                chitiet.Giovao = gioVao;
            }

            await _appDbContext.SaveChangesAsync();

            TempData["Success"] = "Đã xác nhận khách đã đến.";
            return RedirectToAction("DanhSachKhachDaDen");
        }

        public async Task<IActionResult> DanhSachKhachDaDen()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            var list = await _appDbContext.Datbans
                .Where(d =>
                    (d.Trangthaidatban == "Khách đã đến" || d.Trangthaidatban == "Đang dùng bữa" || d.Trangthaidatban == "Đã đặt món")
                    && d.Ngaydat == today)
                
                .Include(d => d.Nguoidung)
                .Include(d => d.IdchinhanhNavigation)
                .Include(d => d.Chitietdatbans)
                    .ThenInclude(ct => ct.IdbanNavigation)
                .ToListAsync();

            return View(list);
        }


        [HttpPost]
        public async Task<IActionResult> EditDatBan(string Iddatban, string Idban, TimeOnly Giobatdau, TimeOnly Gioketthuc)
        {
            try
            {
                var datban = await _appDbContext.Datbans
                    .Include(d => d.Chitietdatbans)
                    .FirstOrDefaultAsync(d => d.Iddatban == Iddatban);

                if (datban == null)
                {
                    TempData["Error"] = "Không tìm thấy đơn đặt bàn.";
                    return RedirectToAction("Index");
                }

                var idChiNhanh = HttpContext.Session.GetString("ChiNhanhId");
                if (datban.Idchinhanh != idChiNhanh)
                {
                    TempData["Error"] = "Bạn không có quyền sửa đơn đặt bàn này.";
                    return RedirectToAction("Index");
                }

                // Nếu giá trị không truyền lên (người dùng không chọn lại), giữ nguyên giá trị cũ
                if (string.IsNullOrEmpty(Idban))
                {
                    Idban = datban.Chitietdatbans.FirstOrDefault()?.Idban;
                }
                if (Giobatdau == default)
                {
                    Giobatdau = datban.Giobatdau;
                }

                // Luôn tính Giờ kết thúc = Giờ bắt đầu + 2 giờ
                Gioketthuc = Giobatdau.AddHours(2);

                // Nếu vượt quá 23h59, bạn có thể tự giới hạn (tùy logic)
                if (Gioketthuc.Hour >= 23)
                {
                    Gioketthuc = new TimeOnly(23, 59);
                }

                // Kiểm tra bàn có bị trùng giờ
                var hasConflict = await _appDbContext.chitietdatbans
                    .Where(ct => ct.Idban == Idban &&
                                ct.IddatbanNavigation.Ngaydat == datban.Ngaydat &&
                                ct.IddatbanNavigation.Iddatban != Iddatban)
                    .AnyAsync(ct =>
                        (Giobatdau < ct.IddatbanNavigation.Gioketthuc && Gioketthuc > ct.IddatbanNavigation.Giobatdau)
                    );

                if (hasConflict)
                {
                    TempData["Error"] = "Bàn hoặc giờ đã được đặt. Vui lòng chọn bàn hoặc giờ khác.";
                    return RedirectToAction("Index");
                }

                // Cập nhật thông tin đặt bàn
                await _appDbContext.Datbans
                    .Where(d => d.Iddatban == Iddatban)
                    .ExecuteUpdateAsync(d => d
                        .SetProperty(x => x.Giobatdau, Giobatdau)
                        .SetProperty(x => x.Gioketthuc, Gioketthuc)
                    );

                // Cập nhật chi tiết bàn
                await _appDbContext.chitietdatbans
                    .Where(ct => ct.Iddatban == Iddatban)
                    .ExecuteDeleteAsync();

                _appDbContext.chitietdatbans.Add(new Chitietdatban
                {
                    Iddatban = Iddatban,
                    Idban = Idban,
                    Giovao = Giobatdau,
                    Giora = Gioketthuc,
                });

                await _appDbContext.SaveChangesAsync();

                TempData["Success"] = "Cập nhật đơn đặt bàn thành công.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Lỗi khi cập nhật: {ex.Message}";
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetEditForm(string Iddatban)
        {
            var datban = await _appDbContext.Datbans
                .Include(d => d.Chitietdatbans)
                .ThenInclude(ct => ct.IdbanNavigation)
                .Include(d => d.IdchinhanhNavigation)
                .FirstOrDefaultAsync(d => d.Iddatban == Iddatban);

            if (datban == null) return NotFound();

            var idChiNhanh = HttpContext.Session.GetString("ChiNhanhId");
            if (datban.Idchinhanh != idChiNhanh)
            {
                return Forbid();
            }

            // Load danh sách bàn theo chi nhánh
            ViewBag.BanList = await _appDbContext.bans
                .Where(b => b.Idchinhanh == idChiNhanh)
                .ToListAsync();

            return PartialView("_EditForm", datban);
        }
    }
}
