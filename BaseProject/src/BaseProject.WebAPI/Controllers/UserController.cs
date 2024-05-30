using BaseProject.Application.Models.Requests;
using BaseProject.Application.Services;
using BaseProject.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("inactive-account/{userId}")]
        public async Task<IActionResult> InActiveAccount(string userId)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _userService.InActiveAccount(userId);
                response.Message = "InActive account successfully";
                response.Data = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPut("update-user/{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UserRequest request)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _userService.UpdateUserAsync(userId, request);
                response.Message = "Update user successfully";
                response.Data = result;
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