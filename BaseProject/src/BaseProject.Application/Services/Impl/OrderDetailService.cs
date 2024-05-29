using AutoMapper;
using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;
using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using Microsoft.Data.SqlClient;

namespace BaseProject.Application.Services.Impl
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderDetailService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(OrderDetailRequest request)
        {
            var orderDetail = new OrderDetail
            {
                OrderId = request.OrderId,
                BookId = request.BookId,
                Quantity = request.Quantity,
                Price = request.Price,
                Discount = request.Discount,
                Total = request.Total
            };

            await _unitOfWork.OrderDetailRepository.AddAsync(orderDetail);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> UpdateAsync(long orderId, long bookId, OrderDetailRequest request)
        {
            var orderDetail = await _unitOfWork.OrderDetailRepository.GetAsync(od => od.OrderId == orderId && od.BookId == bookId);
            if (orderDetail == null)
            {
                return false;
            }

            orderDetail.Quantity = request.Quantity;
            orderDetail.Price = request.Price;
            orderDetail.Discount = request.Discount;
            orderDetail.Total = request.Total;

            _unitOfWork.OrderDetailRepository.Update(orderDetail);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> DeleteAsync(long orderId, long bookId)
        {
            var orderDetail = await _unitOfWork.OrderDetailRepository.GetAsync(od => od.OrderId == orderId && od.BookId == bookId);
            if (orderDetail == null)
            {
                return false;
            }

            _unitOfWork.OrderDetailRepository.Delete(orderDetail);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<IEnumerable<OrderDetailResponse>> GetOrderDetails()
        {
            var lovedBooks = await _unitOfWork.OrderDetailRepository.GetAllAsync();

            return lovedBooks.Select(od => _mapper.Map<OrderDetailResponse>(od));
        }

        public async Task<OrderDetailResponse> GetOrderDetail(long orderId, long bookId)
        {
            var orderDetail = await _unitOfWork.OrderDetailRepository.GetAsync(od => od.OrderId == orderId && od.BookId == bookId);
            if (orderDetail == null)
            {
                return null;
            }
            return _mapper.Map<OrderDetailResponse>(orderDetail);
        }
    }
}