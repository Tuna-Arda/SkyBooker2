namespace FlightService.Models
{
    public class Flight
    {
        public string Id { get; set; }
        public string FlightId { get; set; }
        public string AirlineName { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int AvailableSeats { get; set; }
    }
}
