using BaseProject.Application.Models.Requests;
using BaseProject.Domain.Entities;

namespace BaseProject.Application.Services
{
    public interface IUserService
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<int> ActiveAccount(string email);

        Task<int> InActiveAccount(string email);

        Task<int> UpdateUserAsync(UserRequest user);

        Task<int> AddMoney(string email, decimal amount);
    }
}