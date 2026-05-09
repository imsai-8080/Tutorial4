using Microsoft.AspNetCore.Mvc;
using Tutorial4.Data;
using Tutorial4.Models;

namespace Tutorial4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll([FromQuery] int? minCapacity, [FromQuery] bool? hasProjector, [FromQuery] bool? activeOnly)
        {
            var rooms = MockDataContext.Rooms.AsQueryable();

            if (minCapacity.HasValue)
                rooms = rooms.Where(r => r.Capacity >= minCapacity.Value);

            if (hasProjector.HasValue)
                rooms = rooms.Where(r => r.HasProjector == hasProjector.Value);

            if (activeOnly.HasValue && activeOnly.Value)
                rooms = rooms.Where(r => r.IsActive);

            return Ok(rooms.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var room = MockDataContext.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [HttpGet("building/{buildingCode}")]
        public IActionResult GetByBuilding(string buildingCode)
        {
            var rooms = MockDataContext.Rooms.Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(rooms);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Room room)
        {
            room.Id = MockDataContext.Rooms.Max(r => r.Id) + 1;
            MockDataContext.Rooms.Add(room);
            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Room updatedRoom)
        {
            var room = MockDataContext.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound();

            room.Name = updatedRoom.Name;
            room.BuildingCode = updatedRoom.BuildingCode;
            room.Floor = updatedRoom.Floor;
            room.Capacity = updatedRoom.Capacity;
            room.HasProjector = updatedRoom.HasProjector;
            room.IsActive = updatedRoom.IsActive;

            return Ok(room);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var room = MockDataContext.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound();
            
            if (MockDataContext.Reservations.Any(res => res.RoomId == id))
            {
                return Conflict("Nie można usunąć sali, która posiada rezerwacje.");
            }

            MockDataContext.Rooms.Remove(room);
            return NoContent();
        }
    }
}