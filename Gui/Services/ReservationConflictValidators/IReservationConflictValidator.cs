using Domain.Models;

namespace Gui.Services.ReservationConflictValidators
{
    public interface IReservationConflictValidator
    {
        Task<Reservation> GetConflictingReservation(Reservation newReservation);
    }
}
