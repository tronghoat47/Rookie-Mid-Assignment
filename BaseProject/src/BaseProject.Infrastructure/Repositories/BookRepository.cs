using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using BaseProject.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetBooks(string? name, string? author, int? releaseYearFrom, int? releaseYearTo, string? categoryName, int pageNumber = 1, int pageSize = 10)
        {
            IEnumerable<Book> books = _context.Books
                .Include(b => b.Category)
                .Include(b => b.Ratings)
                .Include(b => b.Comments)
                .Include(b => b.LovedBooks)
                .Where(b => !b.IsDeleted);
            if (!string.IsNullOrEmpty(name))
            {
                books = books.Where(b => b.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(author))
            {
                books = books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
            }

            if (releaseYearFrom.HasValue)
            {
                books = books.Where(b => b.ReleaseYear >= releaseYearFrom.Value);
            }

            if (releaseYearTo.HasValue)
            {
                books = books.Where(b => b.ReleaseYear <= releaseYearTo.Value);
            }

            if (!string.IsNullOrEmpty(categoryName))
            {
                books = books.Where(b => b.Category.Name.Contains(categoryName, StringComparison.OrdinalIgnoreCase));
            }

            return books.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}