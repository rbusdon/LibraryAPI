using Library.Database.Models;
using Microsoft.AspNetCore.Mvc;
using RMLibrary.Database.Gateways.Interfaces;

namespace RMLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthorController : Controller
    {
        private readonly IAuthorGateway _gateway;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(IAuthorGateway gateway, ILogger<AuthorController> logger)
        {
            _gateway = gateway;
            _logger = logger;
        }

        [HttpGet, ActionName("GetAll")]
        public IActionResult Get()
        {
            try
            {
                var messages = _gateway.GetAllAuthors();
                return Ok(messages);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("{id:int}"), ActionName("GetById")]
        public Author GetById(int id) => _gateway.GetAuthorById(id)!;

        [HttpPost]
        public Author Post([FromBody] Author author) => _gateway.CreateAuthor(author);

        [HttpPut("{id:int}"), ActionName("Modify")]
        public IActionResult Edit([FromRoute] int id, [FromBody] Author updatedAuthor)
        {
            try
            {
                var existingAuthor = _gateway.GetAuthorById(id);
                if (existingAuthor == null)
                {
                    return BadRequest("Id is not valid");
                }
                existingAuthor.GivenName = updatedAuthor.GivenName;
                existingAuthor.FamilyName = updatedAuthor.FamilyName;
                existingAuthor.DateOfBirth = updatedAuthor.DateOfBirth;

                _gateway.UpdateAuthor(existingAuthor);

                return Ok(id);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete, ActionName("Delete")]
        public IActionResult Delete([FromBody] int Id)
        {
            if (_gateway.GetAuthorById(Id) is null)
            {
                return BadRequest();
            }
            else
            {
                _gateway.DeleteAuthor(Id);
                return StatusCode(200);
            }
        }
    }
}

//using Dtos;