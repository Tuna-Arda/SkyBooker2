using BookingService.Models;
using BookingService.Repositories;
using BookingService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRabbitMQService _rabbitMQService;
        private readonly IHttpFlightService _httpFlightService;
        private readonly ILogger<BookingController> _logger;

        public BookingController(
            IBookingRepository bookingRepository,
            IRabbitMQService rabbitMQService,
            IHttpFlightService httpFlightService,
            ILogger<BookingController> logger)
        {
            _bookingRepository = bookingRepository;
            _rabbitMQService = rabbitMQService;
            _httpFlightService = httpFlightService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all bookings");
            var bookings = await _bookingRepository.GetAllAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            _logger.LogInformation($"Getting booking with ID: {id}");
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                _logger.LogWarning($"Booking with ID: {id} not found");
                return NotFound();
            }

            return Ok(booking);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] Booking booking)
        {
            _logger.LogInformation("Creating a new booking");
            if (booking == null)
            {
                _logger.LogWarning("Booking data is null");
                return BadRequest();
            }

            // Validate that the flight exists and has enough seats
            bool isValidFlight = await _httpFlightService.ValidateFlightAsync(booking.FlightId, booking.TicketCount);
            if (!isValidFlight)
            {
                _logger.LogWarning($"Flight validation failed for FlightId: {booking.FlightId}, TicketCount: {booking.TicketCount}");
                return BadRequest("Invalid flight or not enough seats available");
            }

            // Create the booking
            booking.CreatedAt = DateTime.Now;
            booking.UpdatedAt = DateTime.Now;
            await _bookingRepository.AddAsync(booking);

            // Update flight seats
            await _httpFlightService.UpdateFlightSeatsAsync(booking.FlightId, booking.TicketCount);

            // Send message to RabbitMQ for notification service
            _rabbitMQService.SendMessage(booking, "booking.created");

            _logger.LogInformation($"Booking created with ID: {booking.Id}");
            return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(string id, [FromBody] Booking booking)
        {
            _logger.LogInformation($"Updating booking with ID: {id}");
            if (booking == null || id != booking.Id)
            {
                _logger.LogWarning($"Booking data is null or ID mismatch. Booking ID: {booking?.Id}, Requested ID: {id}");
                return BadRequest();
            }

            var existingBooking = await _bookingRepository.GetByIdAsync(id);
            if (existingBooking == null)
            {
                _logger.LogWarning($"Booking with ID: {id} not found");
                return NotFound();
            }

            // If ticket count is being increased, check if flight has enough seats
            if (booking.TicketCount > existingBooking.TicketCount)
            {
                int additionalSeats = booking.TicketCount - existingBooking.TicketCount;
                bool isValidFlight = await _httpFlightService.ValidateFlightAsync(booking.FlightId, additionalSeats);
                if (!isValidFlight)
                {
                    _logger.LogWarning($"Flight validation failed for FlightId: {booking.FlightId}, Additional seats: {additionalSeats}");
                    return BadRequest("Not enough seats available for the updated booking");
                }

                // Update flight seats
                await _httpFlightService.UpdateFlightSeatsAsync(booking.FlightId, additionalSeats);
            }

            booking.UpdatedAt = DateTime.Now;
            var result = await _bookingRepository.UpdateAsync(booking);
            if (!result)
            {
                _logger.LogError($"Failed to update booking with ID: {id}");
                return StatusCode(500, "Failed to update the booking");
            }

            // Send message to RabbitMQ for notification service
            _rabbitMQService.SendMessage(booking, "booking.updated");

            _logger.LogInformation($"Booking with ID: {id} updated successfully");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation($"Deleting booking with ID: {id}");
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                _logger.LogWarning($"Booking with ID: {id} not found");
                return NotFound();
            }

            var result = await _bookingRepository.DeleteAsync(id);
            if (!result)
            {
                _logger.LogError($"Failed to delete booking with ID: {id}");
                return StatusCode(500, "Failed to delete the booking");
            }

            // Send message to RabbitMQ for notification service
            _rabbitMQService.SendMessage(booking, "booking.deleted");

            _logger.LogInformation($"Booking with ID: {id} deleted successfully");
            return NoContent();
        }
    }
}
