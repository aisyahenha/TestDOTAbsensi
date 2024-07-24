using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using TestAbsensi.DTO;
using TestAbsensi.Models;
using TestAbsensi.Repositories;
using TestAbsensi.Exeptions;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace TestAbsensi.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<UserModel> _repository;
        private readonly IConnectionMultiplexer _redis;
        public AuthService(IRepository<UserModel> repository, IConnectionMultiplexer redis)
        {
            _repository = repository;
            _redis = redis;
        }
        public async Task<LoginResponseViewModel> Login(LoginViewModel user)
        {
            var findedUser = await _repository.FindBy((obj) => obj.Username == user.Username.ToLower());

            if (findedUser is null) throw new BadRequest("username atau password salah");

            var isVerified = BCrypt.Net.BCrypt.Verify(user.Password, findedUser.Password);

            if (!isVerified) throw new BadRequest("username atau password salah");

            var claims = new List<Claim>
            {
                new Claim("Username", $"{findedUser.Username}-{findedUser.Id}" ),
                new Claim(ClaimTypes.Role,"ADMIN"),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,"ADMIN")
            };
            var claimsIdentity = new ClaimsIdentity(claims);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ini secret key JWT yang panjang sekali hardcode");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var result = new LoginResponseViewModel()
            {
                Token = tokenHandler.WriteToken(token),
                Username = user.Username
            };

            //save token to redis
            var db = _redis.GetDatabase();
            await db.StringSetAsync($"{findedUser.Username}-{findedUser.Id}", tokenHandler.WriteToken(token),new TimeSpan(24,0,0));

            return result;
        }

        public async Task Logout(ClaimsIdentity identity)
        {
            var key = identity.FindFirst("Username");
            var db = _redis.GetDatabase();
            await db.KeyDeleteAsync(key?.Value ?? "");
        }
    }
}
