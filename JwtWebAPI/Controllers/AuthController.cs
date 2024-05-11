using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtWebAPI.Model.Entity;
using JwtWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JwtWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly UserTokenRepository _userTokenRepository;
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public AuthController(IConfiguration configuration, UserRepository userRepository
            , UserTokenRepository userTokenRepository)
        {
            _userRepository = userRepository;
            _userTokenRepository = userTokenRepository;
            _key = configuration["JwtConfig:Key"];
            _issuer = configuration["JwtConfig:Issuer"];
            _audience = configuration["JwtConfig:Audience"];
        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            if (userName != "ali" || password != "123456") return Unauthorized();

            var user = _userRepository.GetUser(2);

            // ایجاد Claims
            var claims = new List<Claim>
            {
                new("UserId",user.Id.ToString()),
                new("Name",user.Name),
                new("PhoneNumber","09910127423")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));// کلید امضای توکن
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);// الگوریتم امضا

            var tokenExp = DateTime.Now.AddMinutes(30);

            // تنظیمات توکن JWT
            var token = new JwtSecurityToken(
                issuer: _issuer, // صادرکننده
                audience: _audience, // دریافت‌کننده
                claims: claims, // لیست Claims
                expires:tokenExp ,  // زمان اعتبار توکن 30 دقیقه
                signingCredentials: creds // اطلاعات امضا
            );

            //ایجاد توکن نهایی برای کاربر
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            _userTokenRepository.SaveToken(new UserToken
            {
                HashedToken = HashHelper.ToHash(jwtToken),
                TokenExp = tokenExp,
                User = user
            });
            return Ok(jwtToken);
        }
    }
}
