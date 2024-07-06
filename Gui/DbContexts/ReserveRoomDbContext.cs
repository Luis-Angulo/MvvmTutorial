using Gui.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Gui.DbContexts
{
    public class ReserveRoomDbContext : DbContext
    {
        public ReserveRoomDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ReservationDTO> Reservations { get; set; }
    }
}
