using BaseProject.Domain.Interfaces;
using BaseProject.Infrastructure.DataAccess;
using BaseProject.Infrastructure.Repositories;

namespace BaseProject.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryContext _context;
        private IUserRepository _userRepository;
        private IRefreshTokenRepository _refreshTokenRepository;
        private IBookRepository _bookRepository;
        private ICategoryRepository _categoryRepository;
        private ILovedBookRepository _lovedBookRepository;
        private ICommentRepository _commentRepository;
        private IBorrowingRepository _borrowingRepository;
        private IBorrowingDetailRepository _borrwingDetailRepository;
        private IRatingRepository _ratingRepository;

        public UnitOfWork(LibraryContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
            => _userRepository ??= new UserRepository(_context);

        public IRefreshTokenRepository RefreshTokenRepository
            => _refreshTokenRepository ??= new RefreshTokenRepository(_context);

        public IBookRepository BookRepository
            => _bookRepository ??= new BookRepository(_context);

        public ICategoryRepository CategoryRepository
            => _categoryRepository ??= new CategoryRepository(_context);

        public ILovedBookRepository LovedBookRepository
            => _lovedBookRepository ??= new LovedBookRepository(_context);

        public ICommentRepository CommentRepository
            => _commentRepository ??= new CommentRepository(_context);

        public IBorrowingRepository BorrowingRepository
                => _borrowingRepository ??= new BorrowingRepository(_context);

        public IBorrowingDetailRepository BorrowingDetailRepository
            => _borrwingDetailRepository ??= new BorrowingDetailRepository(_context);

        public IRatingRepository RatingRepository
            => _ratingRepository ??= new RatingRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}