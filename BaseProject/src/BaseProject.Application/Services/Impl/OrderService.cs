using AutoMapper;
using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;
using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using Microsoft.Data.SqlClient;

namespace BaseProject.Application.Services.Impl
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(OrderRequest orderRequest)
        {
            var order = new Order
            {
                UserId = orderRequest.UserId,
                OrderDate = orderRequest.OrderDate,
                Status = orderRequest.Status,
                Address = orderRequest.Address,
                Phone = orderRequest.Phone,
                Total = orderRequest.Total,
                TotalPayment = orderRequest.TotalPayment,
                DiscountId = orderRequest.DiscountId,
                PaymentMethod = orderRequest.PaymentMethod
            };

            await _unitOfWork.OrderRepository.AddAsync(order);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var order = await _unitOfWork.OrderRepository.GetAsync(o => o.Id == id);
            if (order == null)
            {
                return false;
            }

            _unitOfWork.OrderRepository.Delete(order);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<OrderResponse> GetOrder(long id)
        {
            var order = await _unitOfWork.OrderRepository.GetAsync(o => o.Id == id, o => o.Discount, o => o.User);
            if (order == null)
            {
                return null;
            }

            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<OrderResponse>> GetOrders()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync(o => true, o => o.Discount, o => o.User);
            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }

        public async Task<bool> UpdateAsync(long id, OrderRequest orderRequest)
        {
            var order = await _unitOfWork.OrderRepository.GetAsync(o => o.Id == id);
            if (order == null)
            {
                return false;
            }

            order.UserId = orderRequest.UserId;
            order.OrderDate = orderRequest.OrderDate;
            order.Status = orderRequest.Status;
            order.Address = orderRequest.Address;
            order.Phone = orderRequest.Phone;
            order.Total = orderRequest.Total;
            order.TotalPayment = orderRequest.TotalPayment;
            order.DiscountId = orderRequest.DiscountId;
            order.PaymentMethod = orderRequest.PaymentMethod;

            _unitOfWork.OrderRepository.Update(order);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersByUser(string userId)
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync(o => o.UserId == userId, o => o.Discount, o => o.User);
            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }
    }
}