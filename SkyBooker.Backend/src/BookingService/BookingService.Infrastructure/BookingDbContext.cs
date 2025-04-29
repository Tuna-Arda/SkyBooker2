using Microsoft.EntityFrameworkCore;
using BookingService.Infrastructure.Models;

namespace BookingService.Infrastructure
{
    public class BookingDbContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=sqlserver;Database=BookingDb;User Id=sa;Password=Your_password123;");
        }
    }
}
