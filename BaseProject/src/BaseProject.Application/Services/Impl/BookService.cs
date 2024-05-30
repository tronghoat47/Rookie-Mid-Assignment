using AutoMapper;
using BaseProject.Application.Constants;
using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;
using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BaseProject.Application.Services.Impl
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateBook(BookRequest book)
        {
            if (!await SaveFileAsync(book.Image))
            {
                throw new FileLoadException("Failed to save file");
            }
            var newBook = new Book
            {
                Name = book.Name,
                Author = book.Author,
                Description = book.Description,
                ReleaseYear = book.ReleaseYear,
                Image = FileConstant.URL_PATH + book.Image.FileName,
                CategoryId = book.CategoryId
            };

            await _unitOfWork.BookRepository.AddAsync(newBook);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await _unitOfWork.BookRepository.GetAsync(b => !b.IsDeleted && b.Id == id);
            if (book == null)
            {
                return false;
            }
            _unitOfWork.BookRepository.SoftDelete(book);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<BookResponse> GetBook(int id)
        {
            var book = await _unitOfWork.BookRepository.GetAsync(b => b.Id == id, b => b.Category);
            if (book == null)
            {
                return null;
            }

            return _mapper.Map<BookResponse>(book);
        }

        public async Task<IEnumerable<BookResponse>> GetBooks()
        {
            var books = await _unitOfWork.BookRepository.GetAllAsync(b => !b.IsDeleted, b => b.Category);
            return books.Select(b => _mapper.Map<BookResponse>(b));
        }

        public async Task<bool> UpdateBook(int bookId, BookRequest book)
        {
            if (book.Image != null && !await SaveFileAsync(book.Image))
            {
                throw new FileLoadException("Failed to save file");
            }
            var existingBook = await _unitOfWork.BookRepository.GetAsync(b => !b.IsDeleted && b.Id == bookId);
            if (existingBook == null)
            {
                return false;
            }

            existingBook.Name = book.Name;
            existingBook.Author = book.Author;
            existingBook.Description = book.Description;
            existingBook.ReleaseYear = book.ReleaseYear;
            existingBook.Image = FileConstant.URL_PATH + book.Image.FileName;
            existingBook.CategoryId = book.CategoryId;

            _unitOfWork.BookRepository.Update(existingBook);
            return await _unitOfWork.CommitAsync() > 0;
        }

        private async Task<bool> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }

            try
            {
                var path = FileConstant.ROOT_PATH + file.FileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}