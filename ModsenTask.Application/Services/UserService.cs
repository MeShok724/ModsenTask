using ModsenTask.Core.Entities;
using ModsenTask.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenTask.Application.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
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

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<bool> AddUserAsync(User user)
        {
            await _userRepository.AddUserAsync(user);
            return true;
            //TODO: изменить после добавления регистрации
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

        public async Task<List<UserBook>> GetUserBooksAsync(Guid userId)
        {
            return await _userRepository.GetUserBooksAsync(userId);
        }
    }

}
