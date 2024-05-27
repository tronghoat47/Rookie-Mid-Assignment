using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Services.Impl
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateBook(Book book)
        {
            await _unitOfWork.BookRepository.AddAsync(book);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await _unitOfWork.BookRepository.GetAsync(b => b.Id == id);
            if (book == null)
            {
                return false;
            }

            _unitOfWork.BookRepository.Delete(book);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Book> GetBook(int id)
        {
            return await _unitOfWork.BookRepository.GetAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _unitOfWork.BookRepository.GetAllAsync();
        }

        public async Task<bool> UpdateBook(Book book)
        {
            _unitOfWork.BookRepository.Update(book);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}