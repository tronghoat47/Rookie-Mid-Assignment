using BaseProject.Application.Models.Requests;
using BaseProject.Domain.Entities;

namespace BaseProject.Application.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(UserRegisterRequest userRequest);

        Task<(string token, string refreshToken, string role, string userId)> LoginAsync(string email, string password);

        Task<(string token, string refreshToken, string role, string userId)> RefreshTokenAsync(string refreshToken);

        Task<int> LogoutAsync(string userId);

        //Task<int> ResetPasswordAsync(string email, string newPassword);
    }
}