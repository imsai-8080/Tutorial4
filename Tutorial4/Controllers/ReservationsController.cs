using Microsoft.AspNetCore.Mvc;
using Tutorial4.Data;
using Tutorial4.Models;

namespace Tutorial4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll([FromQuery] DateTime? date, [FromQuery] string? status, [FromQuery] int? roomId)
        {
            var query = MockDataContext.Reservations.AsQueryable();

            if (date.HasValue)
                query = query.Where(r => r.Date.Date == date.Value.Date);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

            if (roomId.HasValue)
                query = query.Where(r => r.RoomId == roomId.Value);

            return Ok(query.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var res = MockDataContext.Reservations.FirstOrDefault(r => r.Id == id);
            if (res == null) return NotFound();
            return Ok(res);
        }
    }
}