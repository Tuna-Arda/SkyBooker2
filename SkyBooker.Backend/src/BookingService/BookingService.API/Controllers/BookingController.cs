using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BookingService.Application;

namespace BookingService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route(""api/booking"")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _svc;
        public BookingController(IBookingService svc) => _svc = svc;

        [HttpPost] public async Task<IActionResult> Create(BookingDto dto)
            => Ok(await _svc.CreateAsync(dto));

        [HttpGet] public async Task<IActionResult> GetAll()
            => Ok(await _svc.GetAllAsync());

        [HttpGet(""{id}"")] public async Task<IActionResult> Get(int id)
            => Ok(await _svc.GetByIdAsync(id));
    }
}
