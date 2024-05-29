using BaseProject.Application.Models.Requests;
using BaseProject.Domain.Entities;

namespace BaseProject.Application.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(UserRequest userRequest);

        Task<(string token, string refreshToken, string role)> LoginAsync(string email, string password);

        Task<(string token, string refreshToken, string role)> RefreshTokenAsync(string refreshToken);

        Task<int> LogoutAsync(string userId);

        Task<int> ResetPasswordAsync(string email, string newPassword);
    }
}