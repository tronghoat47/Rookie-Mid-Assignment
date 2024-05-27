using BaseProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Services
{
    public interface IBookService
    {
        Task<bool> CreateBook(Book book);

        Task<bool> UpdateBook(Book book);

        Task<bool> DeleteBook(int id);

        Task<Book> GetBook(int id);

        Task<IEnumerable<Book>> GetBooks();
    }
}