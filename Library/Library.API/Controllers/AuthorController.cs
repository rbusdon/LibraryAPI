using Library.Database.Models;
using Microsoft.AspNetCore.Mvc;
using RMLibrary.API.DTOs;
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
                var authors = _gateway.GetAllAuthors();
                return Ok(authors);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("{id:int}"), ActionName("GetById")]
        public IActionResult GetById(int id)
        {
            try
            {
                var author = _gateway.GetAuthorById(id);
                var authorDTO = new AuthorDTO(author.FamilyName, author.GivenName, author.DateOfBirth);
                return Ok(authorDTO);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPost, ActionName("Create")]
        public IActionResult Post([FromBody] AuthorDTO authorDTO)
        {
            try
            {
                var author = new Author(null, authorDTO.GivenName, authorDTO.FamilyName, authorDTO.DateOfBirth);
                author = _gateway.CreateAuthor(author);
                return Ok(author);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPut("{id:int}"), ActionName("Modify")]
        public IActionResult Edit([FromRoute] int id, AuthorDTO author)
        {
            try
            {
                var newAuthor = new Author(id, author.GivenName, author.FamilyName, author.DateOfBirth);
                _gateway.UpdateAuthor(newAuthor);
                return Ok(newAuthor);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("{id:int}"), ActionName("Delete")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (_gateway.GetAuthorById(id) is null)
            {
                return BadRequest("Id is not valid");
            }
            else
            {
                _gateway.DeleteAuthor(id);
                return StatusCode(200);
            }
        }
    }
}