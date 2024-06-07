using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;

namespace BaseProject.Application.Services
{
    public interface ILovedBookService
    {
        //Task<IEnumerable<LovedBookResponse>> GetLovedBooks();

        Task<bool> CreateLovedBook(LovedBookRequest lovedBook);

        Task<bool> DeleteLovedBook(string userId, long bookId);

        Task<IEnumerable<LovedBookResponse>> GetLovedBooksByUser(string userId);

        //Task<IEnumerable<LovedBookResponse>> GetLovedBooksByBook(long bookId);

        //Task<LovedBookResponse> GetLovedBook(string userId, long bookId);
    }
}