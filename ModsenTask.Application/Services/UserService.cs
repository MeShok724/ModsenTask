using ModsenTask.Core.Entities;
using ModsenTask.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenTask.Application.Services
{
    public class UserService(IUserRepository userRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                return false;

            await _userRepository.DeleteUserAsync(userId);
            return true;
        }

        public async Task<IEnumerable<UserBook>> GetUserBooksAsync(Guid userId)
        {
            return await _userRepository.GetUserBooksAsync(userId);
        }
    }

}
