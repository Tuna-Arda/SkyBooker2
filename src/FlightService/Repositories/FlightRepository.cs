using FlightService.Models;

namespace FlightService.Repositories
{
    public interface IFlightRepository
    {
        IEnumerable<Flight> GetAll();
        Flight GetById(string id);
        void Add(Flight flight);
        void Update(Flight flight);
        void Delete(string id);
    }

    public class FlightRepository : IFlightRepository
    {
        private readonly List<Flight> _flights = new List<Flight>();

        public FlightRepository()
        {
            // Add some sample flights
            _flights.Add(new Flight
            {
                Id = "1",
                FlightId = "LH123",
                AirlineName = "Lufthansa",
                Source = "Zurich",
                Destination = "Berlin",
                DepartureTime = DateTime.Now.AddDays(5),
                ArrivalTime = DateTime.Now.AddDays(5).AddHours(2),
                AvailableSeats = 120
            });

            _flights.Add(new Flight
            {
                Id = "2",
                FlightId = "SWR456",
                AirlineName = "Swiss",
                Source = "Geneva",
                Destination = "London",
                DepartureTime = DateTime.Now.AddDays(7),
                ArrivalTime = DateTime.Now.AddDays(7).AddHours(2),
                AvailableSeats = 80
            });
        }

        public IEnumerable<Flight> GetAll()
        {
            return _flights;
        }

        public Flight GetById(string id)
        {
            return _flights.FirstOrDefault(f => f.Id == id);
        }

        public void Add(Flight flight)
        {
            _flights.Add(flight);
        }

        public void Update(Flight flight)
        {
            var index = _flights.FindIndex(f => f.Id == flight.Id);
            if (index != -1)
            {
                _flights[index] = flight;
            }
        }

        public void Delete(string id)
        {
            var flight = GetById(id);
            if (flight != null)
            {
                _flights.Remove(flight);
            }
        }
    }
}
