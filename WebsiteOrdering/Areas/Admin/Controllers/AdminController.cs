using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteOrdering.Models;
using WebsiteOrdering.Repositories;
using WebsiteOrdering.Services;
using WebsiteOrdering.ViewModels;

namespace WebsiteOrdering.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("Account")]
    [Route("[area]/[controller]/[action]")]
   // [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly AppDbContext _appDbContext;
        public AdminController(IAccountRepository accountRepository,AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _accountRepository = accountRepository;
          
        }
        public async Task<IActionResult> Index(string maDon, string sđt, string tenNguoiDat)
        {
            List<Datban> result = new List<Datban>();

            if (!string.IsNullOrWhiteSpace(maDon) || !string.IsNullOrWhiteSpace(sđt) || !string.IsNullOrWhiteSpace(tenNguoiDat))
            {
                var query = _appDbContext.Datbans
                    .Include(d => d.Nguoidung)
                    .Include(d => d.IdchinhanhNavigation)
                    .Include(d => d.Chitietdatbans)
                        .ThenInclude(ct => ct.IdbanNavigation)
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


        [HttpGet]
        public IActionResult LoginStaff()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginStaff(LoginViewModel model)
        {
            Console.WriteLine("ModelState.IsValid = " + ModelState.IsValid);
            Console.WriteLine("Email = " + model?.Email);
            Console.WriteLine("Password = " + model?.Password);
            if (!ModelState.IsValid) return View(model);

            var result = await _accountRepository.LoginAsync(model);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng.");
                return View(model);
            }

            var user = await _accountRepository.GetUserByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Không tìm thấy người dùng.");
                return View(model);
            }

            var roles = await _accountRepository.GetUserRolesAsync(user);
            Console.WriteLine("User roles: " + string.Join(", ", roles));
            // Điều hướng theo vai trò
            if (roles.Contains("Admin"))
            {
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }
            else if (roles.Contains("Staff"))
            {
                if (user.IdchinhanhNavigation?.Idchinhanh == null)
                {
                    ModelState.AddModelError("", "Nhân viên chưa được gán chi nhánh.");
                    return View(model);
                }

                // Lưu ID chi nhánh vào session để xử lý sau này
                //HttpContext.Session.SetString("ChiNhanhId", user.IdchinhanhNavigation.Idchinhanh.ToString());

                var signInSuccess = await _accountRepository.SignInStaffWithClaimsAsync(user);
                if (!signInSuccess)
                {
                    ModelState.AddModelError("", "Đăng nhập không thành công.");
                    return View(model);
                }

                return RedirectToAction("Index", "Staff", new { area = "Staff" });
            }
            else
            {
                ModelState.AddModelError("", "Tài khoản không có quyền truy cập vào khu vực này.");
                return View(model);
            }
        }


      //Logout admin chưa được
        [HttpPost]
        public async Task<IActionResult> LogoutAdmin()
        {
            await _accountRepository.LogoutAsync();
            //return Redirect("/");
            return RedirectToAction("Login", "Account");
        }


        //[HttpGet]
        //public async Task<IActionResult> SearchDonDatBan(string maDon,string sđt, string tenNguoiDat)
        //{
        //    if(string.IsNullOrWhiteSpace(maDon)&& string.IsNullOrWhiteSpace(sđt) && string.IsNullOrWhiteSpace(tenNguoiDat))
        //    {
        //        ViewBag.Message = "Vui lòng nhập ít nhất 1 trường để tìm kiếm.";
        //           return View(new List<Datban>() );
        //    }
        //    var query = _appDbContext.Datbans
        //        .Include(d => d.Nguoidung)
        //        .Include(d => d.IdchinhanhNavigation)
        //        .Include(d => d.Chitietdatbans)
        //           .ThenInclude(ct => ct.IdbanNavigation)
        //        .AsQueryable();
        //    if(!string.IsNullOrWhiteSpace(maDon))
        //        query = query.Where(d=>d.Iddatban.Trim().Equals(maDon.Trim(),StringComparison.OrdinalIgnoreCase));
        //    if (!string.IsNullOrWhiteSpace(sđt))
        //        query = query.Where(d => d.Sđtngdat != null && d.Sđtngdat.Contains(sđt.Trim()));
        //    if (!string.IsNullOrWhiteSpace(tenNguoiDat))
        //        query = query.Where(d => d.Tenngdat != null && d.Tenngdat.Contains(tenNguoiDat.Trim()));
        //    var result = await query.OrderByDescending(d=>d.Ngaydat).ToListAsync();
        //    return View(result);
        //}

    }
}
