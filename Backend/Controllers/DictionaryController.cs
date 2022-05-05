using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string Get(string word)
        {
            return "Hello, World!";
        }
    }
}
