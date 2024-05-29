using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;

namespace BaseProject.Application.Services
{
    public interface IBookService
    {
        Task<bool> CreateBook(BookRequest book);

        Task<bool> UpdateBook(int bookId, BookRequest book);

        Task<bool> DeleteBook(int id);

        Task<BookResponse> GetBook(int id);

        Task<IEnumerable<BookResponse>> GetBooks();
    }
}