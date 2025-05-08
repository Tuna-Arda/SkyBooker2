using AuthService.Models;
using BCrypt.Net;

namespace AuthService.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
    }

    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();
        private int _nextId = 1;

        public Task<User?> GetByIdAsync(int id)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.Username == username));
        }

        public Task<User?> CreateUserAsync(User user)
        {
            user.Id = _nextId++;
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _users.Add(user);
            return Task.FromResult<User?>(user);
        }

        public Task<bool> UpdateUserAsync(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser == null)
                return Task.FromResult(false);

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            
            return Task.FromResult(true);
        }

        public Task<bool> DeleteUserAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return Task.FromResult(false);

            _users.Remove(user);
            return Task.FromResult(true);
        }
    }
}
