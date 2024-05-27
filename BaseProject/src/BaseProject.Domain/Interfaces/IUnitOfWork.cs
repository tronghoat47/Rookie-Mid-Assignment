namespace BaseProject.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        IBookRepository BookRepository { get; }
        ILovedBookRepository LovedBookRepository { get; }
        IDiscountRepository DiscountRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        Task<int> CommitAsync();

        int Commit();
    }
}