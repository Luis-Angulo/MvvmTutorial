using System.ComponentModel.DataAnnotations;

namespace Gui.DTOs
{
    public class ReservationDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public int FloorNumber { get; set; }
        public int RoomNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
