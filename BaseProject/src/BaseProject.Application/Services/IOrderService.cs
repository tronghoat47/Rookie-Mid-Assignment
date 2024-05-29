using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;

namespace BaseProject.Application.Services
{
    public interface IOrderService
    {
        Task<bool> CreateAsync(OrderRequest orderRequest);

        Task<bool> UpdateAsync(long id, OrderRequest orderRequest);

        Task<bool> DeleteAsync(long id);

        Task<IEnumerable<OrderResponse>> GetOrders();

        Task<OrderResponse> GetOrder(long id);

        Task<IEnumerable<OrderResponse>> GetOrdersByUser(string userId);
    }
}