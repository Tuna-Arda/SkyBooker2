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

        public FlightController(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var flights = _flightRepository.GetAll();
            return Ok(flights);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var flight = _flightRepository.GetById(id);
            if (flight == null)
                return NotFound();

            return Ok(flight);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] Flight flight)
        {
            if (flight == null)
                return BadRequest();

            flight.Id = Guid.NewGuid().ToString();
            _flightRepository.Add(flight);

            return CreatedAtAction(nameof(GetById), new { id = flight.Id }, flight);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(string id, [FromBody] Flight flight)
        {
            if (flight == null || id != flight.Id)
                return BadRequest();

            var existingFlight = _flightRepository.GetById(id);
            if (existingFlight == null)
                return NotFound();

            _flightRepository.Update(flight);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(string id)
        {
            var flight = _flightRepository.GetById(id);
            if (flight == null)
                return NotFound();

            _flightRepository.Delete(id);
            return NoContent();
        }
    }
}
