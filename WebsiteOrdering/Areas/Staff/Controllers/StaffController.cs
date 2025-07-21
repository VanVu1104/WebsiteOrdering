using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteOrdering.Models;
using WebsiteOrdering.Repositories;

namespace WebsiteOrdering.Areas.Staff.Controllers
{
    //[Authorize(Roles = "Staff")]
    [Area("Staff")]
    [Route("[area]/[controller]/[action]")]
    public class StaffController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IAccountRepository _accountRepository;
        public StaffController(AppDbContext context, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _appDbContext = context;
        }
       
       
        public async Task<IActionResult> Index(string maDon,string sđt,string tenNguoiDat)
        {
            var chiNhanhId = User.FindFirst("ChiNhanhId")?.Value;
            if (string.IsNullOrEmpty(chiNhanhId)) return Unauthorized();
            // Lấy chi nhánh
            var chiNhanh = _appDbContext.chinhanh.FirstOrDefault(c => c.Idchinhanh == chiNhanhId);
            ViewBag.TenChiNhanh = chiNhanh?.Tencnhanh ?? "Không rõ";
            List<Datban> result = new List<Datban>();

            if (!string.IsNullOrWhiteSpace(maDon) || !string.IsNullOrWhiteSpace(sđt) || !string.IsNullOrWhiteSpace(tenNguoiDat))
            {
                var query = _appDbContext.Datbans
                    .Include(d => d.Nguoidung)
                    .Include(d => d.IdchinhanhNavigation)
                    .Include(d => d.Chitietdatbans)
                        .ThenInclude(ct => ct.IdbanNavigation)
                    .Where(d => d.Idchinhanh == chiNhanhId)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(maDon))
                    query = query.Where(d => d.Iddatban != null && d.Iddatban.Trim().ToLower() == maDon.Trim().ToLower());

                if (!string.IsNullOrWhiteSpace(sđt))
                    query = query.Where(d => d.Sđtngdat != null && d.Sđtngdat.Contains(sđt.Trim()));

                if (!string.IsNullOrWhiteSpace(tenNguoiDat))
                    query = query.Where(d => d.Tenngdat != null && d.Tenngdat.Contains(tenNguoiDat.Trim()));

                result = await query.OrderByDescending(d => d.Ngaydat).ToListAsync();
            }

            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> LogoutStaff()
        {
            await _accountRepository.LogoutAsync();
            //return Redirect("/");
            return RedirectToAction("LoginStaff", "Admin", new { area = "Admin" });
        }

    }
}
