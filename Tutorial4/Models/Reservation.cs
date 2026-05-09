using System.ComponentModel.DataAnnotations;

namespace Tutorial4.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Organizator jest wymagany")]
        public string OrganizerName { get; set; }

        [Required(ErrorMessage = "Temat jest wymagany")]
        public string Topic { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public string Status { get; set; } // np. planned, confirmed, cancelled
    }
}