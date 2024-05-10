using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ListController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var list = new Dictionary<string, string>
            {
                { "txt", "notepad.exe" },
                { "bmp", "paint.exe" },
                { "dib", "paint.exe" },
                { "rtf", "wordpad.exe" }
            };

            return Ok(list);
        }
    }
}
