using BaseProject.Domain.Interfaces;
using BaseProject.Infrastructure.DataAccess;
using BaseProject.Infrastructure.Repositories;

namespace BaseProject.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BaseProjectContext _context;
        private IUserRepository _userRepository;
        private IRefreshTokenRepository _refreshTokenRepository;
        private IBookRepository _bookRepository;
        private ICategoryRepository _categoryRepository;
        private IDiscountRepository _discountRepository;
        private IOrderRepository _orderRepository;
        private IOrderDetailRepository _orderDetailRepository;
        private ILovedBookRepository _lovedBookRepository;

        public UnitOfWork(BaseProjectContext context)
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

        public IOrderRepository OrderRepository
            => _orderRepository ??= new OrderRepository(_context);

        public IOrderDetailRepository OrderDetailRepository
            => _orderDetailRepository ??= new OrderDetailRepository(_context);

        public ILovedBookRepository LovedBookRepository
            => _lovedBookRepository ??= new LovedBookRepository(_context);

        public IDiscountRepository DiscountRepository
            => _discountRepository ??= new DiscountRepository(_context);

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