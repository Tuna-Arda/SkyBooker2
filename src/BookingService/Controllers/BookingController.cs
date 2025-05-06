using BookingService.Models;
using BookingService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var bookings = _bookingRepository.GetAll();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var booking = _bookingRepository.GetById(id);
            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] Booking booking)
        {
            if (booking == null)
                return BadRequest();

            booking.Id = Guid.NewGuid().ToString();
            _bookingRepository.Add(booking);

            return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(string id, [FromBody] Booking booking)
        {
            if (booking == null || id != booking.Id)
                return BadRequest();

            var existingBooking = _bookingRepository.GetById(id);
            if (existingBooking == null)
                return NotFound();

            _bookingRepository.Update(booking);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(string id)
        {
            var booking = _bookingRepository.GetById(id);
            if (booking == null)
                return NotFound();

            _bookingRepository.Delete(id);
            return NoContent();
        }
    }
}
