using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;

namespace BaseProject.Application.Services
{
    public interface ILovedBookService
    {
        Task<IEnumerable<LovedBookResponse>> GetLovedBooks();

        Task<bool> CreateLovedBook(LovedBookRequest lovedBook);

        Task<bool> DeleteLovedBook(string userId, long bookId);

        Task<LovedBookResponse> GetLovedBooksByUser(string userId);

        Task<LovedBookResponse> GetLovedBooksByBook(long bookId);
    }
}