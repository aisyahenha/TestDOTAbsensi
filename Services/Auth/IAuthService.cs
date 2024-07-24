using System.Security.Claims;
using TestAbsensi.DTO;
using TestAbsensi.Models;

namespace TestAbsensi.Services.Auth
{
    public interface IAuthService
    {
        Task<LoginResponseViewModel> Login(LoginViewModel user);
        Task Logout(ClaimsIdentity identity);
    }
}
