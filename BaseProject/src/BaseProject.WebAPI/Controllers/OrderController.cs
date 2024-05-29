using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;
using BaseProject.Application.Services;
using BaseProject.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace BaseProject.WebAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response = new GeneralResponse();
            try
            {
                var orders = await _orderService.GetOrders();
                if (orders == null || !orders.Any())
                {
                    response.Success = false;
                    response.Message = "No orders found";
                    return NotFound(response);
                }
                response.Message = "Get orders successfully";
                response.Data = orders;
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
        public async Task<IActionResult> GetOrder(long id)
        {
            var response = new GeneralResponse();
            try
            {
                var order = await _orderService.GetOrder(id);
                if (order == null)
                {
                    response.Success = false;
                    response.Message = "Order not found";
                    return NotFound(response);
                }
                response.Message = "Get order successfully";
                response.Data = order;
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
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest orderRequest)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _orderService.CreateAsync(orderRequest);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Create order failed";
                    return BadRequest(response);
                }
                response.Message = "Create order successfully";
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
        public async Task<IActionResult> UpdateOrder(long id, [FromBody] OrderRequest orderRequest)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _orderService.UpdateAsync(id, orderRequest);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Update order failed";
                    return BadRequest(response);
                }
                response.Message = "Update order successfully";
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
        public async Task<IActionResult> DeleteOrder(long id)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _orderService.DeleteAsync(id);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Delete order failed";
                    return BadRequest(response);
                }
                response.Message = "Delete order successfully";
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
        public async Task<IActionResult> GetOrdersByUser(string userId)
        {
            var response = new GeneralResponse();
            try
            {
                var orders = await _orderService.GetOrdersByUser(userId);
                if (orders == null || !orders.Any())
                {
                    response.Success = false;
                    response.Message = "No orders found";
                    return NotFound(response);
                }
                response.Message = "Get orders successfully";
                response.Data = orders;
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