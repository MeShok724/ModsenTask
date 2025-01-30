using Microsoft.EntityFrameworkCore;
using ModsenTask.Core.Entities;
using ModsenTask.Infrastructure.Persistence;

namespace ModsenTask.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;


        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users
                .Include(u => u.BorrowedBooks)
                .ThenInclude(ub => ub.Book)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<UserBook>> GetUserBooksAsync(Guid userId)
        {
            return await _context.UserBooks
                .Include(ub => ub.Book)
                .Where(ub => ub.UserId == userId)
                .ToListAsync();
        }
    }
}
