using AuthService.Data;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace AuthService.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<User> Register(RegisterModel model);
        Task<bool> UserExists(string username, string email);
    }

    public class UserService : IUserService
    {
        private readonly AuthDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(AuthDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User?> Authenticate(string username, string password)
        {
            _logger.LogInformation($"Attempting to authenticate user: {username}");
            
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
            
            if (user == null)
            {
                _logger.LogWarning($"Authentication failed: User {username} not found");
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                _logger.LogWarning($"Authentication failed: Invalid password for user {username}");
                return null;
            }

            _logger.LogInformation($"User {username} authenticated successfully");
            return user;
        }

        public async Task<User?> Register(RegisterModel model)
        {
            _logger.LogInformation($"Registering new user: {model.Username}");
            
            if (await UserExists(model.Username, model.Email))
            {
                _logger.LogWarning($"Registration failed: User with username {model.Username} or email {model.Email} already exists");
                return null;
            }

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"User {model.Username} registered successfully");
            return user;
        }

        public async Task<bool> UserExists(string username, string email)
        {
            return await _context.Users.AnyAsync(x => x.Username == username || x.Email == email);
        }
    }
}
