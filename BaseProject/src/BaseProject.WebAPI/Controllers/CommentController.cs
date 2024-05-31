using BaseProject.Application.Models.Requests;
using BaseProject.Application.Services;
using BaseProject.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace BaseProject.WebAPI.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetComments()
        {
            var response = new GeneralResponse();
            try
            {
                var comments = await _commentService.GetComments();
                if (comments == null || !comments.Any())
                {
                    response.Success = false;
                    response.Message = "No comments found";
                    return NotFound(response);
                }
                response.Message = "Get comments successfully";
                response.Data = comments.AsQueryable();
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
        public async Task<IActionResult> GetComment(long id)
        {
            var response = new GeneralResponse();
            try
            {
                var comment = await _commentService.GetCommentById(id);
                if (comment == null)
                {
                    response.Success = false;
                    response.Message = "Comment not found";
                    return NotFound(response);
                }
                response.Message = "Get comment successfully";
                response.Data = comment;
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
        public async Task<IActionResult> CreateComment([FromForm] CommentRequest comment)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _commentService.CreateComment(comment);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Create fail";
                    return BadRequest(response);
                }
                response.Message = "Create comment successfully";
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
        public async Task<IActionResult> UpdateComment(long id, [FromForm] CommentRequest comment)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _commentService.UpdateComment(id, comment);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Update fail";
                    return BadRequest(response);
                }
                response.Message = "Update comment successfully";
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
        public async Task<IActionResult> DeleteComment(long id)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _commentService.DeleteComment(id);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Delete fail";
                    return BadRequest(response);
                }
                response.Message = "Delete comment successfully";
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

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCommentsByUserId(string userId)
        {
            var response = new GeneralResponse();
            try
            {
                var comments = await _commentService.GetCommentsByUserId(userId);
                if (comments == null || !comments.Any())
                {
                    response.Success = false;
                    response.Message = "No comments found";
                    return NotFound(response);
                }
                response.Message = "Get comments successfully";
                response.Data = comments.AsQueryable();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetCommentsByBookId(long bookId)
        {
            var response = new GeneralResponse();
            try
            {
                var comments = await _commentService.GetCommentsByBookId(bookId);
                if (comments == null || !comments.Any())
                {
                    response.Success = false;
                    response.Message = "No comments found";
                    return NotFound(response);
                }
                response.Message = "Get comments successfully";
                response.Data = comments.AsQueryable();
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