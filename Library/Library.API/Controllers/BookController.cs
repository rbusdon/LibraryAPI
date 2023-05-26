using Library.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMLibrary.API.DTOs;
using RMLibrary.Database.Gateways;
using RMLibrary.Database.Gateways.Interfaces;
using System;

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

        [HttpGet, ActionName("GetAll")]
        public IActionResult Get()
        {
            try
            {
                var books = _gateway.GetAllBooks();
                return Ok(books);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("{id:int}"), ActionName("GetById")]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                var book = _gateway.GetBookByISBN(id);
                var bookDTO = new BookDTO(book.Title, book.Author, book.Year, book.ISBN);
                return Ok(bookDTO);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPost, ActionName("Create")]
        public IActionResult Post([FromBody] List<BookDTO> bulkOfBookDTO)
        {
            try
            {
                List<Book> newShelf = new List<Book>();
                foreach (BookDTO book in bulkOfBookDTO)
                {
                    var newBook = new Book(null, book.Title, book.Year, book.ISBN);
                    newShelf.Add(newBook);
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
        public IActionResult Edit([FromRoute] int isbn, BookDTO book)
        {
            try
            {
                var newBook = new Book(null, book.Title, book.Year, isbn);
                _gateway.UpdateBook(isbn, newBook);
                return Ok(newBook);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("{id:int}"), ActionName("Delete")]
        public IActionResult Delete([FromRoute] int ISBN)
        {
            if (_gateway.GetBookByISBN(ISBN) is null)
            {
                return BadRequest("Id is not valid");
            }
            else
            {
                _gateway.DeleteBook(ISBN);
                return StatusCode(200);
            }
        }
    }
}
