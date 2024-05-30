using BaseProject.Application.Models.Requests;
using BaseProject.Application.Services;
using BaseProject.Domain.Models;
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
            var response = new GeneralResponse();
            try
            {
                var books = await _bookService.GetBooks();
                if (books == null || !books.Any())
                {
                    response.Success = false;
                    response.Message = "No books found";
                    return NotFound(response);
                }
                response.Message = "Get books successfully";
                response.Data = books;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var response = new GeneralResponse();
            try
            {
                var book = await _bookService.GetBook(id);
                if (book == null)
                {
                    response.Success = false;
                    response.Message = "Book not found";
                    return NotFound(response);
                }
                response.Message = "Get book successfully";
                response.Data = book;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromForm] BookRequest book)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _bookService.CreateBook(book);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Create fail";
                    return BadRequest(response);
                }
                response.Message = "Create book successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromForm] BookRequest book)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _bookService.UpdateBook(id, book);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Update fail";
                    return BadRequest(response);
                }
                response.Message = "Update book successfully";
                response.Data = id;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _bookService.DeleteBook(id);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Delete fail";
                    return BadRequest(response);
                }
                response.Message = "Delete book successfully";
                response.Data = id;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
    }
}