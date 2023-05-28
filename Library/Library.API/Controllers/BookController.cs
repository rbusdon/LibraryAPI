using Library.Database.Models;
using Microsoft.AspNetCore.Mvc;
using RMLibrary.API.DTOs;
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

        [HttpGet("{size:int}/{page:int}"), ActionName("GetAll")]
        public IActionResult Get([FromRoute] int size, [FromRoute] int page)
        {
            try
            {
                var books = _gateway.GetAllBooks(size, page).ToList();
                return Ok(books);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("{isbn:int}"), ActionName("GetByISBN")]
        public IActionResult GetByISBN([FromRoute] int isbn)
        {
            try
            {
                var book = _gateway.GetBookByISBN(isbn);
                var bookDTO = new BookDTO(book.Title, book.AuthorId, book.Year, book.ISBN);
                return Ok(bookDTO);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPost, ActionName("Create")]
        public IActionResult Post([FromBody] List<BookDTO> newShelfDTO)
        {
            try
            {
                List<Book> newShelf = new List<Book>();
                foreach (BookDTO book in newShelfDTO)
                {
                    newShelf.Add(new Book
                    {
                        Title = book.Title,
                        Year = book.Year,
                        ISBN = book.ISBN,
                        AuthorId = book.AuthorId
                    });
                }
                _gateway.CreateBook(newShelf);
                return Ok(newShelf);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPut("{isbn:int}"), ActionName("Modify")]
        public IActionResult Edit([FromRoute] int isbn, [FromBody] BookDTO book)
        {
            try
            {
                var oldBook = _gateway.GetBookByISBN(isbn);
                var newBook = new Book
                {
                    Id = oldBook.Id,
                    Title = book.Title,
                    AuthorId = book.AuthorId,
                    Year = book.Year,
                    ISBN = isbn
                };
                _gateway.UpdateBook(isbn, newBook);
                return Ok(book);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("{isbn:int}"), ActionName("Delete")]
        public IActionResult Delete([FromRoute] int ISBN)
        {
            try
            {
                _gateway.DeleteBook(ISBN);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }
    }
}
