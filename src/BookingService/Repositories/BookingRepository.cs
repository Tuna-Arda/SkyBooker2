using BookingService.Models;

namespace BookingService.Repositories
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAll();
        Booking GetById(string id);
        void Add(Booking booking);
        void Update(Booking booking);
        void Delete(string id);
    }

    public class BookingRepository : IBookingRepository
    {
        private readonly List<Booking> _bookings = new List<Booking>();

        public BookingRepository()
        {
            // Add some sample bookings
            _bookings.Add(new Booking
            {
                Id = "1",
                FlightId = "1",
                PassengerId = "1",
                PassengerFirstname = "Max",
                PassengerLastname = "Mustermann",
                TicketCount = 2,
                BookingDate = DateTime.Now.AddDays(-3)
            });
        }

        public IEnumerable<Booking> GetAll()
        {
            return _bookings;
        }

        public Booking GetById(string id)
        {
            return _bookings.FirstOrDefault(b => b.Id == id);
        }

        public void Add(Booking booking)
        {
            _bookings.Add(booking);
        }

        public void Update(Booking booking)
        {
            var index = _bookings.FindIndex(b => b.Id == booking.Id);
            if (index != -1)
            {
                _bookings[index] = booking;
            }
        }

        public void Delete(string id)
        {
            var booking = GetById(id);
            if (booking != null)
            {
                _bookings.Remove(booking);
            }
        }
    }
}
