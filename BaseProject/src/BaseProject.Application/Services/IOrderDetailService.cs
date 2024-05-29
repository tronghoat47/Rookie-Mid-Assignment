using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;

namespace BaseProject.Application.Services
{
    public interface IOrderDetailService
    {
        Task<bool> CreateAsync(OrderDetailRequest request);

        Task<bool> UpdateAsync(long orderId, long bookId, OrderDetailRequest request);

        Task<bool> DeleteAsync(long orderId, long bookId);

        Task<IEnumerable<OrderDetailResponse>> GetOrderDetails();

        Task<OrderDetailResponse> GetOrderDetail(long orderId, long bookId);
    }
}