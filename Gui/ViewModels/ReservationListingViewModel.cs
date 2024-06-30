using Gui.ViewModels.Abstractions;
using Gui.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Gui.ViewModels
{
    public class ReservationListingViewModel : ViewModelBase
    {
        private ObservableCollection<ReservationViewModel> _Reservations;
        public IEnumerable<ReservationViewModel> Reservations => _Reservations;
        public ICommand MakeReservationCommand { get; }
        public ReservationListingViewModel()
        {
            _Reservations = new ObservableCollection<ReservationViewModel>();
            MakeReservationCommand = new NavigateCommand();
            // TODO: Remove Test Data
            _Reservations.Add(new ReservationViewModel(new("Luis", new(12, 24), DateTime.Now, DateTime.Now)));
            _Reservations.Add(new ReservationViewModel(new("Paco", new(12, 32), DateTime.Now, DateTime.Now)));
            _Reservations.Add(new ReservationViewModel(new("Pablo", new(12, 16), DateTime.Now, DateTime.Now)));
        }
    }
}
