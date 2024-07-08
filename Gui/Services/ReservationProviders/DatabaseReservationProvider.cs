using Domain.Models;
using Gui.DbContexts;
using Gui.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Gui.Services.ReservationProviders
{
    public class DatabaseReservationProvider : IReservationProvider
    {
        private readonly ReserveRoomDbContextFactory _dbContextFactory;

        public DatabaseReservationProvider(ReserveRoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            using(var dbContext = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ReservationDTO> dtos = await dbContext.Reservations.ToListAsync();

                // TEMP - Create artifical delay to show loading animations
                await Task.Delay(2000);

                // Mapping DTOs to entities with LINQ
                return dtos.Select(r => new Reservation(
                    r.UserName
                    ,new(r.FloorNumber, r.RoomNumber)
                    ,r.StartTime
                    ,r.EndTime)
                );
            }
        }
    }
}
