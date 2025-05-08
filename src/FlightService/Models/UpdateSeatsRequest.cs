using System.ComponentModel.DataAnnotations;

namespace FlightService.Models
{
    public class UpdateSeatsRequest
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Ticket count must be greater than 0")]
        public int TicketCount { get; set; }
    }
}
