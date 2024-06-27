using Domain.Models;
using Gui.ViewModels.Abstractions;

namespace Gui.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
        private Reservation _Reservation;
        public string UserName => _Reservation.UserName;
        public string RoomId => _Reservation.RoomId.ToString();
        public string StartDate => _Reservation.StartTime.ToString("d");
        public string EndDate => _Reservation.EndTime.ToString("d");

        public ReservationViewModel(Reservation r)
        {
            _Reservation = r;
        }
    }
}
