using AuthService.Models;

namespace AuthService.Repositories
{
    public interface IUserRepository
    {
        User GetByUsername(string username);
        void Add(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public UserRepository()
        {
            // Add some test users
            _users.Add(new User
            {
                Id = "1",
                Username = "testuser",
                Email = "test@example.com",
                Password = BCrypt.Net.BCrypt.HashPassword("password")
            });
        }

        public User GetByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public void Add(User user)
        {
            _users.Add(user);
        }
    }
}
