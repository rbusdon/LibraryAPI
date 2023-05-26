using Microsoft.AspNetCore.Mvc;

namespace RMLibrary.API.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
