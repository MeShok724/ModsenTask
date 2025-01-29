using ModsenTask.Core.Entities;

namespace ModsenTask.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task DeleteUserAsync(Guid userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<UserBook>> GetUserBooksAsync(Guid userId);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(Guid userId);
        Task UpdateUserAsync(User user);
    }
}