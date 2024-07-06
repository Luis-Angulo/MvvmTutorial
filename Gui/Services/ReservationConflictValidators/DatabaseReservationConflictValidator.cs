using Domain.Models;
using Gui.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Gui.Services.ReservationConflictValidators
{
    public class DatabaseReservationConflictValidator : IReservationConflictValidator
    {
        private readonly ReserveRoomDbContextFactory _dbContextFactory;

        public DatabaseReservationConflictValidator(ReserveRoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Reservation> GetConflictingReservation(Reservation newRes)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var dto = await dbContext.Reservations
                    .Where(r => r.FloorNumber == newRes.RoomId.FloorNumber)
                    .Where(r => r.RoomNumber == newRes.RoomId.RoomNumber)
                    .Where(r => r.EndTime > newRes.StartTime)
                    .Where(r => r.StartTime < newRes.EndTime)
                    .FirstOrDefaultAsync();
                if (dto == null)
                {
                    return null;
                }
                return new Reservation(
                    dto.UserName
                    , new(dto.FloorNumber, dto.RoomNumber)
                    , dto.StartTime
                    , dto.EndTime);
            }
        }
    }
}
