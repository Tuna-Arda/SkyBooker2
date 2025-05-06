namespace BookingService.Models
{
    public class Booking
    {
        public string Id { get; set; }
        public string FlightId { get; set; }
        public string PassengerId { get; set; }
        public string PassengerFirstname { get; set; }
        public string PassengerLastname { get; set; }
        public int TicketCount { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
    }
}
