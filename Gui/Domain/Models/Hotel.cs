namespace Domain.Models
{
    public class Hotel
    {
        private readonly ReservationBook _ReservationBook;
        public string Name { get; }
        public Hotel(string name, ReservationBook reservationBook)
        {
            Name = name;
            _ReservationBook = reservationBook;
        }
        public async Task<IEnumerable<Reservation>> GetAllReservations()
            => await _ReservationBook.GetAllReservations();

        public async Task MakeReservation(Reservation reservation)
            => await _ReservationBook.AddReservation(reservation);

    }
}
