using Domain.Models;

namespace Gui.Services.ReservationCreators
{
    public interface IReservationCreator
    {
        Task CreateReservation(Reservation r);
    }
}
