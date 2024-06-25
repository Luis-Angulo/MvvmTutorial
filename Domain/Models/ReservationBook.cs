using Domain.Exceptions;

namespace Domain.Models
{
    public class ReservationBook
    {
        private readonly List<Reservation> _Reservations;

        public ReservationBook()
        {
            _Reservations = [];
        }
        public IEnumerable<Reservation> GetAllReservations() =>
            _Reservations;

        public void AddReservation(Reservation newReservation)
        {
            // Check all reservations, if a conflict is found, throw an exception
            foreach (var existingReservation in _Reservations)
            {
                if (existingReservation.Conflicts(newReservation))
                {
                    throw new ReservationConflictException(existingReservation, newReservation);
                }
            }
            // else add the new reservation
            _Reservations.Add(newReservation);
        }


    }
}
