using BookingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Booking>()
                .Property(b => b.FlightId)
                .IsRequired();

            modelBuilder.Entity<Booking>()
                .Property(b => b.PassengerId)
                .IsRequired();

            modelBuilder.Entity<Booking>()
                .Property(b => b.PassengerFirstname)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Booking>()
                .Property(b => b.PassengerLastname)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Booking>()
                .Property(b => b.TicketCount)
                .IsRequired();

            modelBuilder.Entity<Booking>()
                .Property(b => b.BookingDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
