using Microsoft.AspNetCore.Identity;
using WebsiteOrdering.ViewModels;

namespace WebsiteOrdering.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
    }
}
