using BaseProject.Application.Models.Requests;
using BaseProject.Application.Services;
using BaseProject.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.WebAPI.Controllers
{
    [Route("api/loved-books")]
    [ApiController]
    public class LovedBookController : BaseApiController
    {
        private readonly ILovedBookService _lovedBookService;

        public LovedBookController(ILovedBookService lovedBookService)
        {
            _lovedBookService = lovedBookService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetLovedBooks()
        //{
        //    var response = new GeneralResponse();
        //    try
        //    {
        //        var lovedBooks = await _lovedBookService.GetLovedBooks();
        //        if (lovedBooks == null || !lovedBooks.Any())
        //        {
        //            response.Success = false;
        //            response.Message = "No loved books found";
        //            return NotFound(response);
        //        }
        //        response.Message = "Get loved books successfully";
        //        response.Data = lovedBooks.ToList();
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Success = false;
        //        response.Message = ex.Message;
        //        return Conflict(response);
        //    }
        //}

        //[HttpGet("{userId}/{bookId}")]
        //public async Task<IActionResult> GetLovedBook(string userId, long bookId)
        //{
        //    var response = new GeneralResponse();
        //    try
        //    {
        //        var lovedBook = await _lovedBookService.GetLovedBook(userId, bookId);
        //        if (lovedBook == null)
        //        {
        //            response.Success = false;
        //            response.Message = "Loved book not found";
        //            return NotFound(response);
        //        }
        //        response.Message = "Get loved book successfully";
        //        response.Data = lovedBook;
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Success = false;
        //        response.Message = ex.Message;
        //        return Conflict(response);
        //    }
        //}

        //[HttpGet("book/{bookId}")]
        //public async Task<IActionResult> GetLovedBooksByBook(long bookId)
        //{
        //    var response = new GeneralResponse();
        //    try
        //    {
        //        var lovedBook = await _lovedBookService.GetLovedBooksByBook(bookId);
        //        if (lovedBook == null)
        //        {
        //            response.Success = false;
        //            response.Message = "Loved book not found";
        //            return NotFound(response);
        //        }
        //        response.Message = "Get loved book successfully";
        //        response.Data = lovedBook.ToList();
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Success = false;
        //        response.Message = ex.Message;
        //        return Conflict(response);
        //    }
        //}

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetLovedBooksByUser(string userId)
        {
            var response = new GeneralResponse();
            try
            {
                var lovedBook = await _lovedBookService.GetLovedBooksByUser(userId);
                if (lovedBook == null)
                {
                    response.Success = false;
                    response.Message = "Loved book not found";
                    return NotFound(response);
                }
                response.Message = "Get loved book successfully";
                response.Data = lovedBook.ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return Conflict(response);
            }
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> CreateLovedBook([FromBody] LovedBookRequest lovedBookRequest)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _lovedBookService.CreateLovedBook(lovedBookRequest);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Create loved book failed";
                    return Conflict(response);
                }
                response.Message = "Create loved book successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return Conflict(response);
            }
        }

        [HttpDelete("{userId}/{bookId}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> DeleteLovedBook(string userId, long bookId)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _lovedBookService.DeleteLovedBook(userId, bookId);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Delete loved book failed";
                    return Conflict(response);
                }
                response.Message = "Delete loved book successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return Conflict(response);
            }
        }
    }
}