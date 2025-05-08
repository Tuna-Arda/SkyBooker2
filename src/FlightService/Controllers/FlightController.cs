using FlightService.Models;
using FlightService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightService.Controllers
{
    [Route("api/flight")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightRepository _flightRepository;
        private readonly ILogger<FlightController> _logger;

        public FlightController(IFlightRepository flightRepository, ILogger<FlightController> logger)
        {
            _flightRepository = flightRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all flights");
            var flights = await _flightRepository.GetAllAsync();
            return Ok(flights);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            _logger.LogInformation($"Getting flight with ID: {id}");
            var flight = await _flightRepository.GetByIdAsync(id);
            if (flight == null)
            {
                _logger.LogWarning($"Flight with ID: {id} not found");
                return NotFound();
            }

            return Ok(flight);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] Flight flight)
        {
            _logger.LogInformation("Creating a new flight");
            await _flightRepository.AddAsync(flight);
            return CreatedAtAction(nameof(GetById), new { id = flight.Id }, flight);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(string id, [FromBody] Flight flight)
        {
            _logger.LogInformation($"Updating flight with ID: {id}");
            if (id != flight.Id)
                return BadRequest();

            var existingFlight = await _flightRepository.GetByIdAsync(id);
            if (existingFlight == null)
                return NotFound();

            var result = await _flightRepository.UpdateAsync(id, flight);
            if (!result)
                return StatusCode(500);

            return NoContent();
        }

        [HttpPut("{id}/seats")]
        [Authorize]
        public async Task<IActionResult> UpdateSeats(string id, [FromBody] UpdateSeatsRequest request)
        {
            _logger.LogInformation($"Updating seats for flight with ID: {id}");
            var result = await _flightRepository.UpdateAvailableSeatsAsync(id, request.TicketCount);
            if (!result)
                return BadRequest("Not enough seats available or flight not found");

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation($"Deleting flight with ID: {id}");
            var flight = await _flightRepository.GetByIdAsync(id);
            if (flight == null)
            {
                _logger.LogWarning($"Flight with ID: {id} not found");
                return NotFound();
            }

            var result = await _flightRepository.DeleteAsync(id);
            if (!result)
            {
                _logger.LogError($"Failed to delete flight with ID: {id}");
                return StatusCode(500, "Failed to delete the flight");
            }
            
            _logger.LogInformation($"Flight with ID: {id} deleted successfully");
            return NoContent();
        }
    }
}
