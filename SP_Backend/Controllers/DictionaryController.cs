using Microsoft.AspNetCore.Mvc;

namespace SP_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DictionaryController : Controller
    {
        [HttpGet("/")]
        public string Get()
        {
            return "Software Project - Hello, World!";
        }
    }
}
