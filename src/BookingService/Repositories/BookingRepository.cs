using BookingService.Data;
using BookingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking> GetByIdAsync(string id);
        Task<Booking> AddAsync(Booking booking);
        Task<bool> UpdateAsync(Booking booking);
        Task<bool> DeleteAsync(string id);
    }

    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext _context;

        public BookingRepository(BookingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking> GetByIdAsync(string id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task<Booking> AddAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> UpdateAsync(Booking booking)
        {
            booking.UpdatedAt = DateTime.Now;
            _context.Entry(booking).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BookingExists(booking.Id))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return false;
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> BookingExists(string id)
        {
            return await _context.Bookings.AnyAsync(e => e.Id == id);
        }
    }
}
