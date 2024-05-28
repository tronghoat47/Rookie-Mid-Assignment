using AutoMapper;
using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;
using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using BaseProject.Infrastructure.UnitOfWork;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /*Task<bool> CreateAsync(OrderRequest orderRequest);
        Task<bool> UpdateAsync(long id, OrderRequest orderRequest);
        Task<bool> DeleteAsync(long id);

        Task<IEnumerable<OrderResponse>> GetOrders();

        Task<OrderResponse> GetOrder(long id);*/

        public async Task<bool> CreateAsync(OrderRequest orderRequest)
        {
            try
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
            catch (SqlException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetAsync(o => o.Id == id);
                if (order == null)
                {
                    throw new KeyNotFoundException("Order not found");
                }

                _unitOfWork.OrderRepository.Delete(order);
                return await _unitOfWork.CommitAsync() > 0;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public async Task<OrderResponse> GetOrder(long id)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetAsync(o => o.Id == id, o => o.Discount);
                if (order == null)
                {
                    throw new KeyNotFoundException("Order not found");
                }

                return _mapper.Map<OrderResponse>(order);
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<OrderResponse>> GetOrders()
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetAllAsync(o => true, o => o.Discount);
                return _mapper.Map<IEnumerable<OrderResponse>>(orders);
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(long id, OrderRequest orderRequest)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetAsync(o => o.Id == id);
                if (order == null)
                {
                    throw new KeyNotFoundException("Order not found");
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
            catch (SqlException)
            {
                return false;
            }
        }
    }
}