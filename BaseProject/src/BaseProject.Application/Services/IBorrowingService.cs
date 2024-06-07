using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;

namespace BaseProject.Application.Services
{
    public interface IBorrowingService
    {
        Task<bool> CreateAsync(BorrowingRequest request);

        Task<bool> UpdateStatusAsync(long id, BorrowingUpdateStatusRequest request);

        Task<BorrowingResponse> GetByIdAsync(long id);

        Task<IEnumerable<BorrowingResponse>> GetByRequestorIdAsync(string requestorId);

        //Task<IEnumerable<BorrowingResponse>> GetByApproverIdAsync(string approverId);

        Task<IEnumerable<BorrowingResponse>> GetAllAsync();

        Task<bool> DeleteAsync(long id);
    }
}