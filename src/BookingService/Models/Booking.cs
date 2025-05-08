using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingService.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string FlightId { get; set; }

        [Required]
        public string PassengerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string PassengerFirstname { get; set; }

        [Required]
        [MaxLength(100)]
        public string PassengerLastname { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int TicketCount { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
