using ModsenTask.Contracts.DTOs;

namespace ModsenTask.Application.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> LoginAsync(LoginRequest request);
        Task<AuthResponse?> RefreshTokenAsync(string email, string refreshToken);
        Task<AuthResponse?> RegisterAsync(RegisterRequest request);
    }
}