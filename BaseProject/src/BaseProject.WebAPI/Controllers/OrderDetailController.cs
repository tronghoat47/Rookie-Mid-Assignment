using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;
using BaseProject.Application.Services;
using BaseProject.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.WebAPI.Controllers
{
    [Route("api/order-details")]
    [ApiController]
    public class OrderDetailController : Controller
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderDetails()
        {
            var response = new GeneralResponse();
            try
            {
                var orderDetails = await _orderDetailService.GetOrderDetails();
                if (orderDetails == null || !orderDetails.Any())
                {
                    response.Success = false;
                    response.Message = "No order details found";
                    return NotFound(response);
                }
                response.Message = "Get order details successfully";
                response.Data = orderDetails;
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
        public async Task<IActionResult> CreateOrderDetail([FromBody] OrderDetailRequest request)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _orderDetailService.CreateAsync(request);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Create order detail failed";
                    return BadRequest(response);
                }
                response.Message = "Create order detail successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPut("{orderId}/{bookId}")]
        public async Task<IActionResult> UpdateOrderDetail(long orderId, long bookId, [FromBody] OrderDetailRequest request)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _orderDetailService.UpdateAsync(orderId, bookId, request);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Update order detail failed";
                    return BadRequest(response);
                }
                response.Message = "Update order detail successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpDelete("{orderId}/{bookId}")]
        public async Task<IActionResult> DeleteOrderDetail(long orderId, long bookId)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _orderDetailService.DeleteAsync(orderId, bookId);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Delete order detail failed";
                    return BadRequest(response);
                }
                response.Message = "Delete order detail successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("{orderId}/{bookId}")]
        public async Task<IActionResult> GetOrderDetail(long orderId, long bookId)
        {
            var response = new GeneralResponse();
            try
            {
                var orderDetail = await _orderDetailService.GetOrderDetail(orderId, bookId);
                if (orderDetail == null)
                {
                    response.Success = false;
                    response.Message = "No order detail found";
                    return NotFound(response);
                }
                response.Message = "Get order detail successfully";
                response.Data = orderDetail;
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