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

        
    }
}