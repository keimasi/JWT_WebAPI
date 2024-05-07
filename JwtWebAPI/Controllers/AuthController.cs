    using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JwtWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public AuthController(IConfiguration configuration)
        {
            _key = configuration["JwtConfig:Key"];
            _issuer = configuration["JwtConfig:Issuer"];
            _audience = configuration["JwtConfig:Audience"];
        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            if (userName == "ali" && password == "123456")
            {
                // ایجاد Claims
                var claims = new List<Claim>
                {
                    new("UserId",Guid.NewGuid().ToString()),
                    new("Name","Alireza Keimasi"),
                    new("PhoneNumber","09910127423")
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));// کلید امضای توکن
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);// الگوریتم امضا

                // تنظیمات توکن JWT
                var token = new JwtSecurityToken(
                    issuer: _issuer, // صادرکننده
                    audience: _audience, // دریافت‌کننده
                    claims: claims, // لیست Claims
                    expires: DateTime.Now.AddMinutes(30),  // زمان اعتبار توکن 30 دقیقه
                    signingCredentials: creds // اطلاعات امضا
                );

                //ایجاد توکن نهایی برای کاربر
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(jwtToken);
            }

            return Unauthorized();
        }
    }
}
