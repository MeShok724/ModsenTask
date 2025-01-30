using ModsenTask.Core.Entities;

namespace ModsenTask.Application.Services
{
    public interface IUserService
    {
        Task<bool> AddUserAsync(User user);
        Task<bool> DeleteUserAsync(Guid userId);
        Task<List<User>> GetAllUsersAsync();
        Task<List<UserBook>> GetUserBooksAsync(Guid userId);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(Guid userId);
        Task UpdateUserAsync(User user);
    }
}