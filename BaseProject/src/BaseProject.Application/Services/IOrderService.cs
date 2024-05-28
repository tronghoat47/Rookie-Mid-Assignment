using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Services
{
    public interface IOrderService
    {
        Task<bool> CreateAsync(OrderRequest orderRequest);

        Task<bool> UpdateAsync(long id, OrderRequest orderRequest);

        Task<bool> DeleteAsync(long id);

        Task<IEnumerable<OrderResponse>> GetOrders();

        Task<OrderResponse> GetOrder(long id);
    }
}