using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestAbsensi.DTO;
using TestAbsensi.Models;
using TestAbsensi.Services.Auth;
using TestAbsensi.Services.Karyawan;

namespace TestAbsensi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<Response<LoginResponseViewModel>> Login(LoginViewModel payload)
        {
            LoginResponseViewModel result = await _authService.Login(payload);
            return new Response<LoginResponseViewModel>()
            {
                Status = true,
                Message = "Login Sukses",
                Data = result
            };
        }

        [HttpPost("logout")]
        public async Task<Response<string>> Logout()
        {
            await _authService.Logout((ClaimsIdentity)User.Identity);
            return new Response<string>()
            {
                Status = true,
                Message = "Logout Sukses",
                Data = ""
            };
        }

    }
}
