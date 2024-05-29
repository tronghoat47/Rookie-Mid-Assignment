using AutoMapper;
using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;
using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using Microsoft.Data.SqlClient;

namespace BaseProject.Application.Services.Impl
{
    public class DiscountService : IDiscountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DiscountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateDiscountAsync(DiscountRequest discountRequest)
        {
            var discount = new Discount
            {
                Code = discountRequest.Code,
                Quantity = discountRequest.Quantity,
                Value = discountRequest.Value,
                StartDate = discountRequest.StartDate,
                EndDate = discountRequest.EndDate,
                IsActive = discountRequest.IsActive
            };
            await _unitOfWork.DiscountRepository.AddAsync(discount);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> DeleteDiscountAsync(long id)
        {
            var discount = await _unitOfWork.DiscountRepository.GetAsync(d => d.Id == id);
            if (discount == null)
            {
                return false;
            }

            _unitOfWork.DiscountRepository.Delete(discount);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<DiscountResponse> GetDiscountByIdAsync(long id)
        {
            var discount = await _unitOfWork.DiscountRepository.GetAsync(d => d.Id == id);
            if (discount == null)
            {
                return null;
            }
            return _mapper.Map<DiscountResponse>(discount);
        }

        public async Task<IEnumerable<DiscountResponse>> GetDiscountsAsync()
        {
            var discounts = await _unitOfWork.DiscountRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DiscountResponse>>(discounts);
        }

        public async Task<bool> UpdateDiscountAsync(long discountId, DiscountRequest discountRequest)
        {
            var discount = await _unitOfWork.DiscountRepository.GetAsync(d => d.Id == discountId);
            if (discount == null)
            {
                return false;
            }

            discount.Code = discountRequest.Code;
            discount.Quantity = discountRequest.Quantity;
            discount.Value = discountRequest.Value;
            discount.StartDate = discountRequest.StartDate;
            discount.EndDate = discountRequest.EndDate;
            discount.IsActive = discountRequest.IsActive;

            _unitOfWork.DiscountRepository.Update(discount);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}