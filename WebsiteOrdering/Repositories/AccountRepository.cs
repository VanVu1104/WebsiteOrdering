using Microsoft.AspNetCore.Identity;
using WebsiteOrdering.Models;
using WebsiteOrdering.ViewModels;

namespace WebsiteOrdering.Repositories
{
    public class AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

    public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        return await _userManager.CreateAsync(user, model.Password);
    }

    public async Task<SignInResult> LoginAsync(LoginViewModel model)
    {
        return await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
}
