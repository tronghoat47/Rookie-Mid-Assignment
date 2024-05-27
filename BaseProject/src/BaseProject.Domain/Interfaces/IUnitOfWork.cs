namespace BaseProject.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        IBookRepository BookRepository { get; }

        Task<int> CommitAsync();

        int Commit();
    }
}