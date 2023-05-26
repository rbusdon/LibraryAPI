using Microsoft.AspNetCore.Mvc;
using RMLibrary.Database.Gateways.Interfaces;

namespace RMLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookController : Controller
    {
        private readonly IBookGateway _gateway;
        private readonly ILogger<BookController> _logger;
        public BookController(IBookGateway gateway, ILogger<BookController> logger)
        {
            _gateway = gateway;
            _logger = logger;
        }
    }
}
