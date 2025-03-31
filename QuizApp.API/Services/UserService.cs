using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuizApp.API.Domain.Models;
using QuizApp.API.Domain.Repositories;

namespace QuizApp.API.Services
{
    public interface IUserService
    {
        Task<User> AuthenticateUserAsync(string username, string password);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly Dictionary<string, string> _hardcodedUsers;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _hardcodedUsers = new Dictionary<string, string>
            {
                { "admin", "admin123" },
                { "developer", "dev123" },
                { "user", "user123" }
            };
        }

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            if (_hardcodedUsers.TryGetValue(username, out var storedPassword) && storedPassword == password)
            {
                var user = await _userRepository.GetByUsernameAsync(username);
                if (user == null)
                {
                    user = new User
                    {
                        Username = username,
                        Email = $"{username}@example.com",
                        Role = username == "admin" ? "Admin" : username == "developer" ? "Developer" : "User",
                        CreatedAt = DateTime.UtcNow,
                        LastLoginAt = DateTime.UtcNow
                    };
                    await _userRepository.CreateAsync(user);
                }
                else
                {
                    user.LastLoginAt = DateTime.UtcNow;
                    await _userRepository.UpdateAsync(user);
                }
                return user;
            }
            return null;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username);
        }
    }
}