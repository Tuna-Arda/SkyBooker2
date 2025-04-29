using Microsoft.EntityFrameworkCore;
using AuthService.Infrastructure.Entities;

namespace AuthService.Infrastructure
{
    public class AuthDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=auth.db");
        }
    }
}
