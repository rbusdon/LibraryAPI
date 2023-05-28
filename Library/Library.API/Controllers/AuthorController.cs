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
            _logger.LogInformation("Using GetAllAuthors");
            try
            {
                var authors = _gateway.GetAllAuthors().ToList();
                return Ok(authors);
            }
            catch
            {
                _logger.LogWarning("Error occurred for GetAllAuthors");
                return Problem();
            }
        }

        [HttpGet("{id:int}"), ActionName("GetById")]
        public IActionResult GetById(int id)
        {
            _logger.LogInformation("Using GetAuthorById");
            try
            {
                var author = _gateway.GetAuthorById(id);
                var authorDTO = new AuthorDTO(author.FamilyName, author.GivenName, author.DateOfBirth);
                return Ok(authorDTO);
            }
            catch
            {
                _logger.LogWarning("Error occurred for GetAuthorById");
                return Problem();
            }
        }

        [HttpPost, ActionName("Create")]
        public IActionResult Post([FromBody] AuthorDTO authorDTO)
        {
            _logger.LogInformation("Using CreateAuthor");
            try
            {
                var author = new Author(null, authorDTO.GivenName, authorDTO.FamilyName, authorDTO.DateOfBirth);
                author = _gateway.CreateAuthor(author);
                return Ok(author);
            }
            catch
            {
                _logger.LogWarning("Error occurred for CreateAuthor");
                return Problem();
            }
        }

        [HttpPut("{id:int}"), ActionName("Modify")]
        public IActionResult Edit([FromRoute] int id, AuthorDTO author)
        {
            _logger.LogInformation("Using UpdateAuthor");
            try
            {
                var newAuthor = new Author(id, author.GivenName, author.FamilyName, author.DateOfBirth);
                _gateway.UpdateAuthor(newAuthor);
                return Ok(newAuthor);
            }
            catch
            {
                _logger.LogWarning("Error occurred for UpdateAuthor");
                return Problem();
            }
        }

        [HttpDelete("{id:int}"), ActionName("Delete")]
        public IActionResult Delete([FromRoute] int id)
        {
            _logger.LogInformation("Using DeleteAuthor");
            try
            {
                if (_gateway.GetAuthorById(id) is null)
                {
                    throw new NotImplementedException();
                }
                _gateway.DeleteAuthor(id);
                return Ok();
            }
            catch
            {
                _logger.LogWarning("Error occurred for DeleteAuthor");
                return Problem();
            }
        }
    }
}