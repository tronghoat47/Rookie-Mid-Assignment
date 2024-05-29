using BaseProject.Application.Models.Requests;
using BaseProject.Application.Services;
using BaseProject.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.WebAPI.Controllers
{
    [Route("api/discounts")]
    [ApiController]
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDiscounts()
        {
            var response = new GeneralResponse();
            try
            {
                var discounts = await _discountService.GetDiscountsAsync();
                if (discounts == null || !discounts.Any())
                {
                    response.Success = false;
                    response.Message = "No discounts found";
                    return NotFound(response);
                }
                response.Message = "Get discounts successfully";
                response.Data = discounts;
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
        public async Task<IActionResult> GetDiscount(int id)
        {
            var response = new GeneralResponse();
            try
            {
                var discount = await _discountService.GetDiscountByIdAsync(id);
                if (discount == null)
                {
                    response.Success = false;
                    response.Message = "Discount not found";
                    return NotFound(response);
                }
                response.Message = "Get discount successfully";
                response.Data = discount;
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
        public async Task<IActionResult> CreateDiscount([FromBody] DiscountRequest discount)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _discountService.CreateDiscountAsync(discount);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Create discount failed";
                    return BadRequest(response);
                }
                response.Message = "Create discount successfully";
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
        public async Task<IActionResult> UpdateDiscount(int id, [FromBody] DiscountRequest discount)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _discountService.UpdateDiscountAsync(id, discount);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Update discount failed";
                    return BadRequest(response);
                }
                response.Message = "Update discount successfully";
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
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _discountService.DeleteDiscountAsync(id);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Delete discount failed";
                    return BadRequest(response);
                }
                response.Message = "Delete discount successfully";
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