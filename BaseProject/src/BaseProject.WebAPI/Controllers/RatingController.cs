using BaseProject.Application.Models.Requests;
using BaseProject.Application.Services;
using BaseProject.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.WebAPI.Controllers
{
    [Route("api/ratings")]
    [ApiController]
    public class RatingController : Controller
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRatings()
        {
            var response = new GeneralResponse();
            try
            {
                var ratings = await _ratingService.GetRatings();
                if (ratings == null || !ratings.Any())
                {
                    response.Success = false;
                    response.Message = "No ratings found";
                    return NotFound(response);
                }
                response.Message = "Get ratings successfully";
                response.Data = ratings;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("{userId}/{bookId}")]
        public async Task<IActionResult> GetRating(string userId, long bookId)
        {
            var response = new GeneralResponse();
            try
            {
                var rating = await _ratingService.GetRatingByUserIdAndBookId(userId, bookId);
                if (rating == null)
                {
                    response.Success = false;
                    response.Message = "Rating not found";
                    return NotFound(response);
                }
                response.Message = "Get rating successfully";
                response.Data = rating;
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
        public async Task<IActionResult> CreateRating([FromBody] RatingRequest ratingRequest)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _ratingService.CreateRating(ratingRequest);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Create rating failed";
                    return BadRequest(response);
                }
                response.Message = "Create rating successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPut("{userId}/{bookId}")]
        public async Task<IActionResult> UpdateRating(string userId, long bookId, [FromBody] RatingRequest ratingRequest)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _ratingService.UpdateRating(userId, bookId, ratingRequest);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Update rating failed";
                    return BadRequest(response);
                }
                response.Message = "Update rating successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpDelete("{userId}/{bookId}")]
        public async Task<IActionResult> DeleteRating(string userId, long bookId)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _ratingService.DeleteRating(userId, bookId);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Delete rating failed";
                    return BadRequest(response);
                }
                response.Message = "Delete rating successfully";
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
        public async Task<IActionResult> GetRatingsByUserId(string userId)
        {
            var response = new GeneralResponse();
            try
            {
                var ratings = await _ratingService.GetRatingsByUserId(userId);
                if (ratings == null || !ratings.Any())
                {
                    response.Success = false;
                    response.Message = "No ratings found";
                    return NotFound(response);
                }
                response.Message = "Get ratings successfully";
                response.Data = ratings;
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
        public async Task<IActionResult> GetRatingsByBookId(long bookId)
        {
            var response = new GeneralResponse();
            try
            {
                var ratings = await _ratingService.GetRatingsByBookId(bookId);
                if (ratings == null || !ratings.Any())
                {
                    response.Success = false;
                    response.Message = "No ratings found";
                    return NotFound(response);
                }
                response.Message = "Get ratings successfully";
                response.Data = ratings;
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