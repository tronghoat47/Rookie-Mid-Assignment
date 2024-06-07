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

        Task<IEnumerable<BookResponse>> GetBooks(FormFilterBook formFilter);

        Task<IEnumerable<BookResponse>> GetTop10NewsBook();

        Task<IEnumerable<BookResponse>> GetTop10LovedBook();

        Task<IEnumerable<BookResponse>> GetNotBorrowedBooks(long borrowingId);
    }
}