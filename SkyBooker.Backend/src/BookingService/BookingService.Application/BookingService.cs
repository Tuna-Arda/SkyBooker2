using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BookingService.Application;
using BookingService.Infrastructure;
using BookingService.Infrastructure.Models;
using FlightService.Application;

namespace BookingService.Application
{
    public interface IBookingService
    {
        Task<Booking> CreateAsync(BookingDto dto);
        Task<Booking[]> GetAllAsync();
        Task<Booking> GetByIdAsync(int id);
    }

    public class BookingService : IBookingService
    {
        private readonly BookingDbContext _db;
        private readonly HttpClient _flightClient;

        public BookingService(BookingDbContext db, HttpClient flightClient)
        {
            _db = db;
            _flightClient = flightClient;
        }

        public async Task<Booking> CreateAsync(BookingDto dto)
        {
            var flight = await _flightClient.GetFromJsonAsync<FlightDto>($"api/flight/{dto.FlightId}");
            if (flight is null)
                throw new Exception("Flug nicht gefunden");

            if (flight.AvailableSeats < dto.TicketCount)
                throw new Exception("Nicht genügend Plätze verfügbar");

            var booking = new Booking
            {
                FlightId = dto.FlightId,
                PassengerId = dto.PassengerId,
                PassengerFirstname = dto.PassengerFirstname,
                PassengerLastname = dto.PassengerLastname,
                TicketCount = dto.TicketCount,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _db.Bookings.Add(booking);
            flight.AvailableSeats -= dto.TicketCount;

            // Optional: FlightService updaten
            await _flightClient.PutAsJsonAsync($"api/flight/{dto.FlightId}", flight);
            await _db.SaveChangesAsync();

            return booking;
        }

        public async Task<Booking[]> GetAllAsync()
        {
            return await _db.Bookings.ToArrayAsync();
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return await _db.Bookings.FindAsync(id)
                   ?? throw new Exception("Buchung nicht gefunden");
        }
    }
}
