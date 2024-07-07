using Domain.Models;

namespace Gui.Stores
{
    // HotelStore works as a cache of Reservation data to prevent excessive requerying of the database
    public class HotelStore
    {
        private readonly List<Reservation> _reservations;
        private readonly Hotel _hotel;
        public IEnumerable<Reservation> Reservations => _reservations;
        public HotelStore(Hotel hotel)
        {
            _reservations = new List<Reservation>();
            _hotel = hotel;
        }
        public async Task Load()
        {
            IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();

            _reservations.Clear();
            _reservations.AddRange(reservations);
        }
    }
}
