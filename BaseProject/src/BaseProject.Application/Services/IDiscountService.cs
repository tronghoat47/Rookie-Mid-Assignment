using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;

namespace BaseProject.Application.Services
{
    public interface IDiscountService
    {
        Task<IEnumerable<DiscountResponse>> GetDiscountsAsync();

        Task<DiscountResponse> GetDiscountByIdAsync(long id);

        Task<bool> CreateDiscountAsync(DiscountRequest discountRequest);

        Task<bool> UpdateDiscountAsync(long discountId, DiscountRequest discountRequest);

        Task<bool> DeleteDiscountAsync(long id);
    }
}