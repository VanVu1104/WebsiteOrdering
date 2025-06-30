using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebsiteOrdering.Models;
using WebsiteOrdering.Repositories;
using WebsiteOrdering.Services;
using WebsiteOrdering.ViewModels;
using Microsoft.AspNetCore.Authentication.Google;

namespace WebsiteOrdering.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISmsService _smsService;
        private readonly IOtpService _otpService;


        public AccountController(IAccountRepository accountRepository, ISmsService smsService,
            IOtpService otpService)
        {
            _accountRepository = accountRepository;
            _smsService = smsService;
            _otpService = otpService;
        }
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var confirmEmailUrl = Url.Action("ConfirmEmail", "Account", null, protocol: Request.Scheme);

            var result = await _accountRepository.RegisterAsync(model, confirmEmailUrl);
            if (result.Succeeded)
            {
                ViewBag.EmailConfirmationMessage = "Đăng ký thành công! Vui lòng kiểm tra email để xác nhận tài khoản.";
                return View();
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }
        [Route("Login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _accountRepository.LoginAsync(model);
            if (result.Succeeded)
                return Redirect(returnUrl ?? "/");

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);

        }
 

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {

            await _accountRepository.LogoutAsync();
            return Redirect("/");
        }
      
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await _accountRepository.ConfirmEmailAsync(userId, token);

            ViewBag.ConfirmEmailMessage = result
                ? "✅ Xác nhận email thành công. Bạn có thể quay lại trang đăng nhập."
                : "❌ Xác nhận thất bại hoặc liên kết không hợp lệ.";

            return View();
        }
        [HttpGet("RegisterPhoneNumber")]
        public ActionResult RegisterPhoneNumber()
        {
            return View();
        }
        [HttpPost("SendOtp")]
        public async Task<IActionResult> SendOtp(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                ModelState.AddModelError("PhoneNumber", "Số điện thoại không được để trống.");
                return View("RegisterPhoneNumber");
            }
            var user = await _accountRepository.GetUserByPhoneNumberAsync(phone);
            if (user != null && user.PhoneNumberConfirmed)
            {
                await _accountRepository.SignInUserAsync(user);
                return RedirectToAction("Index", "Home");
            }

            var (success, createdUser, errors) = await _accountRepository.CreateUserWithPhone(phone);
            if (!success || createdUser == null)
            {
                foreach (var error in errors!) ModelState.AddModelError("", error);
                return View("RegisterPhoneNumber", phone);
            }

            var otp = _otpService.GenerateOtp(phone);
            var message = $"Ma OTP cua ban la: {otp}";
            var sendSuccess = await _smsService.SendSmsAsync(phone, message);

            if (!sendSuccess)
            {
                ModelState.AddModelError("", "Không gửi được SMS.");
                return View("RegisterPhoneNumber", phone);
            }

            return View("VerifyOtp", new VerifyOtpViewModel
            {
                PhoneNumber = phone
            });
        }

        [HttpPost("VerifyOtp")]
        public async Task<IActionResult> VerifyOtp(VerifyOtpViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!_otpService.VerifyOtp(model.PhoneNumber, model.OtpInput))
            {
                ModelState.AddModelError("", "Mã OTP không đúng hoặc đã hết hạn.");
                return View(model);
            }

            var user = await _accountRepository.GetUserByPhoneNumberAsync(model.PhoneNumber);
            if (user == null)
            {
                ModelState.AddModelError("", "Không tìm thấy người dùng.");
                return View(model);
            }

            user.PhoneNumberConfirmed = true;
            await _accountRepository.UpdateUserAsync(user);
            await _accountRepository.SignInUserAsync(user);

            return RedirectToAction("Index", "Home");
        }
        // Đăng nhập bằng Google
        [Route("login-google")]
        public IActionResult LoginWithGoogle()
        {
            var redirectUrl = Url.Action("GoogleResponse", "Account");
            var properties = _accountRepository.GooglelLoginAsync(GoogleDefaults.AuthenticationScheme, redirectUrl);
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await _accountRepository.GoogleLoginCallbackAsync();
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            TempData["Error"] = "Đăng nhập bằng Google thất bại.";
            return RedirectToAction("Login");
        }

    }
}
