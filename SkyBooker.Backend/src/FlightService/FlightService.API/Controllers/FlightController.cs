using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FlightService.Application;

namespace FlightService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route(""api/flight"")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _svc;
        public FlightController(IFlightService svc) => _svc = svc;

        [HttpPost] public async Task<IActionResult> Create(FlightDto dto)
            => Ok(await _svc.CreateAsync(dto));

        [HttpGet] public async Task<IActionResult> GetAll()
            => Ok(await _svc.GetAllAsync());

        [HttpGet(""{id}"")] public async Task<IActionResult> Get(string id)
            => Ok(await _svc.GetByIdAsync(id));
    }
}
