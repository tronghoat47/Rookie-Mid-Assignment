using BaseProject.Application.Models.Requests;
using BaseProject.Domain.Entities;

namespace BaseProject.Application.Services
{
    public interface IUserService
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<User> GetUserByIdAsync(string userId);

        Task<int> ActiveAccount(string email);

        Task<int> InActiveAccount(string userId);

        Task<int> UpdateUserAsync(string userId, UserRequest user);
    }
}