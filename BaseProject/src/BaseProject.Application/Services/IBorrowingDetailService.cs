using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;
using BaseProject.Domain.Constants;

namespace BaseProject.Application.Services
{
    public interface IBorrowingDetailService
    {
        Task<bool> CreateAsync(BorrowingDetailRequest request);

        Task<bool> UpdateStatusAsync(long borrowingId, long bookId, BorrowingDetailUpdateStatusRequest request);

        Task<bool> UpdateStatusExtendAsync(long borrowingId, long bookId, BorrowingDetailUpdateStatusExtendRequest request);

        Task<bool> UpdateReturnedAtAsync(long borrowingId, long bookId, BorrowingDetailUpdateReturnedAtRequest request);

        Task<bool> DeleteAsync(long borrowingId, long bookId);

        Task<IEnumerable<BorrowingDetailResponse>> GetBorrowingDetailsAsync();

        Task<BorrowingDetailResponse> GetBorrowingDetailAsync(long borrowingId, long bookId);
    }
}