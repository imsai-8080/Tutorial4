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

        [HttpPost]
        public IActionResult Create([FromBody] Reservation newRes)
        {
            if (newRes.EndTime <= newRes.StartTime)
                return BadRequest("Czas zakończenia musi być późniejszy niż rozpoczęcia.");

            
            var room = MockDataContext.Rooms.FirstOrDefault(r => r.Id == newRes.RoomId);
            if (room == null) return BadRequest("Wybrana sala nie istnieje.");
            
            if (!room.IsActive)
                return BadRequest("Nie można rezerwować nieaktywnej sali.");
            
            bool hasConflict = MockDataContext.Reservations.Any(r => 
                r.RoomId == newRes.RoomId && 
                r.Date.Date == newRes.Date.Date &&
                r.StartTime < newRes.EndTime && 
                newRes.StartTime < r.EndTime);

            if (hasConflict)
                return Conflict("Sala jest już zarezerwowana w tym czasie.");

            newRes.Id = MockDataContext.Reservations.Any() ? MockDataContext.Reservations.Max(r => r.Id) + 1 : 1;
            MockDataContext.Reservations.Add(newRes);
            
            return CreatedAtAction(nameof(GetById), new { id = newRes.Id }, newRes);
        }

        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Reservation updatedRes)
        {
            var res = MockDataContext.Reservations.FirstOrDefault(r => r.Id == id);
            if (res == null) return NotFound();
            
            res.OrganizerName = updatedRes.OrganizerName;
            res.Topic = updatedRes.Topic;
            res.Date = updatedRes.Date;
            res.StartTime = updatedRes.StartTime;
            res.EndTime = updatedRes.EndTime;
            res.Status = updatedRes.Status;

            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = MockDataContext.Reservations.FirstOrDefault(r => r.Id == id);
            if (res == null) return NotFound();

            MockDataContext.Reservations.Remove(res);
            return NoContent();
        }
    }
}