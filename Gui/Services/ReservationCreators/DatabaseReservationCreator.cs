using Domain.Models;
using Gui.DbContexts;
using Gui.DTOs;

namespace Gui.Services.ReservationCreators
{
    public class DatabaseReservationCreator : IReservationCreator
    {
        private readonly ReserveRoomDbContextFactory _dbContextFactory;

        public DatabaseReservationCreator(ReserveRoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateReservation(Reservation r)
        {
            using(var dbContext = _dbContextFactory.CreateDbContext())
            {
                var resDTO = new ReservationDTO() {
                    UserName = r.UserName
                    , EndTime = r.EndTime
                    , StartTime = r.StartTime
                    , FloorNumber = r.RoomId?.FloorNumber ?? 0
                    , RoomNumber = r.RoomId?.RoomNumber ?? 0
                    , Id = new Guid()
                };
                dbContext.Reservations.Add(resDTO);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
