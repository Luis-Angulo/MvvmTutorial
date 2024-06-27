using Domain.Models;
using Gui.ViewModels.Abstractions;

namespace Gui.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
        private Reservation _Reservation;
        public string UserName => _Reservation.UserName;
        public string RoomId => _Reservation.RoomId.ToString();
        public DateTime StartTime => _Reservation.StartTime;
        public DateTime EndTime => _Reservation.EndTime;

        public ReservationViewModel(Reservation r)
        {
            _Reservation = r;
        }
    }
}
