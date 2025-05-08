using FlightService.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightService.Repositories
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetAllAsync();
        Task<Flight> GetByIdAsync(string id);
        Task<Flight> AddAsync(Flight flight);
        Task<bool> UpdateAsync(string id, Flight flight);
        Task<bool> DeleteAsync(string id);
        Task<bool> UpdateAvailableSeatsAsync(string id, int seatCount);
    }

    public class FlightRepository : IFlightRepository
    {
        private readonly IMongoCollection<Flight> _flights;

        public FlightRepository(IMongoClient mongoClient, IConfiguration configuration)
        {
            var database = mongoClient.GetDatabase(configuration["MongoDB:DatabaseName"]);
            _flights = database.GetCollection<Flight>(configuration["MongoDB:CollectionName"]);
        }

        public async Task<IEnumerable<Flight>> GetAllAsync()
        {
            return await _flights.Find(flight => true).ToListAsync();
        }

        public async Task<Flight> GetByIdAsync(string id)
        {
            return await _flights.Find(flight => flight.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Flight> AddAsync(Flight flight)
        {
            await _flights.InsertOneAsync(flight);
            return flight;
        }

        public async Task<bool> UpdateAsync(string id, Flight flight)
        {
            flight.UpdatedAt = DateTime.Now;
            var result = await _flights.ReplaceOneAsync(f => f.Id == id, flight);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _flights.DeleteOneAsync(flight => flight.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<bool> UpdateAvailableSeatsAsync(string id, int seatCount)
        {
            var flight = await GetByIdAsync(id);
            if (flight == null || flight.AvailableSeats < seatCount)
                return false;

            flight.AvailableSeats -= seatCount;
            flight.UpdatedAt = DateTime.Now;
            return await UpdateAsync(id, flight);
        }
    }
}
