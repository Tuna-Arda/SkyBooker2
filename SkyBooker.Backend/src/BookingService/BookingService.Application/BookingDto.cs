namespace BookingService.Application
{
    public class BookingDto
    {
        public string FlightId { get; set; }
        public int PassengerId { get; set; }
        public string PassengerFirstname { get; set; }
        public string PassengerLastname { get; set; }
        public int TicketCount { get; set; }
    }
}
