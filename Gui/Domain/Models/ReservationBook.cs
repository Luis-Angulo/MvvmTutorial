using Domain.Exceptions;
using Gui.Services.ReservationConflictValidators;
using Gui.Services.ReservationCreators;
using Gui.Services.ReservationProviders;

namespace Domain.Models
{
    public class ReservationBook
    {
        private readonly IReservationProvider _reservationProvider;
        private readonly IReservationCreator _reservationCreator;
        private readonly IReservationConflictValidator _reservationConflictValidator;

        public ReservationBook(IReservationProvider reservationProvider, IReservationCreator reservationCreator, IReservationConflictValidator reservationConflictValidator)
        {
            _reservationProvider = reservationProvider;
            _reservationCreator = reservationCreator;
            _reservationConflictValidator = reservationConflictValidator;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations() =>
            await _reservationProvider.GetAllReservations();

        public async Task AddReservation(Reservation newReservation)
        {
            var conflicting = await _reservationConflictValidator.GetConflictingReservation(newReservation);

            if (conflicting != null)
            {
                throw new ReservationConflictException(conflicting, newReservation);
            }
            // else add the new reservation
            await _reservationCreator.CreateReservation(newReservation);
        }


    }
}
