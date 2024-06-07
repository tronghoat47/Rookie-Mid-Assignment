using BaseProject.Domain.Entities;

namespace BaseProject.Application.Services
{
    public interface ICartService
    {
        Task<bool> CreateAsync(Cart cart);

        Task<bool> DeleteAsync(string userId, int bookId);

        Task<bool> DeleteByBookId(int bookId);

        //Task<Cart> GetCartAsync(string userId, int bookId);

        Task<IEnumerable<Cart>> GetByUserAsync(string userId);
    }
}