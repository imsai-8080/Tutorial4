using Tutorial4.Models;

namespace Tutorial4.Data
{
    public static class MockDataContext
    {
        public static List<Room> Rooms = new List<Room>
        {
            new Room { Id = 1, Name = "Sala A1", BuildingCode = "A", Floor = 1, Capacity = 30, HasProjector = true, IsActive = true },
            new Room { Id = 2, Name = "Laboratorium 101", BuildingCode = "B", Floor = 1, Capacity = 15, HasProjector = true, IsActive = true },
            new Room { Id = 3, Name = "Sala Konferencyjna", BuildingCode = "A", Floor = 2, Capacity = 50, HasProjector = true, IsActive = true },
            new Room { Id = 4, Name = "Mały Gabinet", BuildingCode = "C", Floor = 0, Capacity = 5, HasProjector = false, IsActive = true },
            new Room { Id = 5, Name = "Nieaktywna Sala", BuildingCode = "B", Floor = 3, Capacity = 20, HasProjector = false, IsActive = false }
        };

        public static List<Reservation> Reservations = new List<Reservation>
        {
            new Reservation { Id = 1, RoomId = 1, OrganizerName = "Jan Kowalski", Topic = "Szkolenie BHP", Date = new DateTime(2026, 05, 10), StartTime = new TimeSpan(08, 0, 0), EndTime = new TimeSpan(10, 0, 0), Status = "confirmed" },
            new Reservation { Id = 2, RoomId = 2, OrganizerName = "Anna Nowak", Topic = "Programowanie C#", Date = new DateTime(2026, 05, 10), StartTime = new TimeSpan(12, 0, 0), EndTime = new TimeSpan(14, 0, 0), Status = "planned" },
            new Reservation { Id = 3, RoomId = 1, OrganizerName = "Marek Wiśniewski", Topic = "Zarządzanie czasem", Date = new DateTime(2026, 05, 11), StartTime = new TimeSpan(09, 0, 0), EndTime = new TimeSpan(11, 0, 0), Status = "confirmed" }
        };
    }
}