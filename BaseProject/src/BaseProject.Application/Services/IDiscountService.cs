using BaseProject.Application.Models.Requests;
using BaseProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Services
{
    public interface IDiscountService
    {
        Task<IEnumerable<Discount>> GetDiscountsAsync();

        Task<Discount> GetDiscountByIdAsync(long id);

        Task<bool> CreateDiscountAsync(DiscountRequest discountRequest);

        Task<bool> UpdateDiscountAsync(long discountId, DiscountRequest discountRequest);

        Task<bool> DeleteDiscountAsync(long id);
    }
}