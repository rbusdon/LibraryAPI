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
        public IActionResult Edit([FromRoute] int id)
        {
            try
            {
                var existingAuthor = _gateway.GetAuthorById(id);
                if (existingAuthor == null)
                {
                    return BadRequest("Id is not valid");
                }
                var author = new Author(null, existingAuthor.GivenName, existingAuthor.FamilyName, existingAuthor.DateOfBirth);

                _gateway.UpdateAuthor(existingAuthor);

                return Ok(existingAuthor);
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
                return BadRequest("Id is not valid");
            }
            else
            {
                _gateway.DeleteAuthor(Id);
                return StatusCode(200);
            }
        }
    }
}