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
            var list = new Dictionary<string, string>();

            list.Add("txt", "notepad.exe");
            list.Add("bmp", "paint.exe");
            list.Add("dib", "paint.exe");
            list.Add("rtf", "wordpad.exe");

            return Ok(list);
        }
    }
}
