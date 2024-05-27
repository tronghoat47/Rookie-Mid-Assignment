using BaseProject.Application.Services;
using BaseProject.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace BaseProject.WebAPI.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : BaseApiController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetBooks();
            return Ok(books.AsQueryable());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookService.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            var result = await _bookService.CreateBook(book);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            var bookToUpdate = await _bookService.GetBook(id);
            bookToUpdate.Name = book.Name;
            bookToUpdate.Author = book.Author;
            bookToUpdate.Description = book.Description;
            bookToUpdate.ReleaseYear = book.ReleaseYear;

            var result = await _bookService.UpdateBook(bookToUpdate);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBook(id);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}