using BaseProject.Application.Models.Requests;
using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Services.Impl
{
    public class DiscountService : IDiscountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DiscountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateDiscountAsync(DiscountRequest discountRequest)
        {
            try
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
            catch (SqlException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteDiscountAsync(long id)
        {
            try
            {
                var discount = await _unitOfWork.DiscountRepository.GetAsync(d => d.Id == id);
                if (discount == null)
                {
                    throw new KeyNotFoundException("Discount not found");
                }

                _unitOfWork.DiscountRepository.Delete(discount);
                return await _unitOfWork.CommitAsync() > 0;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public async Task<Discount> GetDiscountByIdAsync(long id)
        {
            try
            {
                return await _unitOfWork.DiscountRepository.GetAsync(d => d.Id == id);
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Discount>> GetDiscountsAsync()
        {
            try
            {
                return await _unitOfWork.DiscountRepository.GetAllAsync();
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public async Task<bool> UpdateDiscountAsync(long discountId, DiscountRequest discountRequest)
        {
            try
            {
                var discount = await _unitOfWork.DiscountRepository.GetAsync(d => d.Id == discountId);
                if (discount == null)
                {
                    throw new KeyNotFoundException("Discount not found");
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
            catch (SqlException)
            {
                return false;
            }
        }
    }
}