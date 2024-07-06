using Domain.Models;
using Gui.Services;
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
        public ICommand LoadReservationsCommand { get; }
        private ReservationListingViewModel(Hotel hotel, NavigationService navigationService)
        {
            _Reservations = new ObservableCollection<ReservationViewModel>();

            MakeReservationCommand = new NavigateCommand(navigationService);
            LoadReservationsCommand = new LoadReservationsCommand(hotel, this);
        }
        public static ReservationListingViewModel LoadViewModel(Hotel hotel, NavigationService navigationService)
        {
            var viewModel = new ReservationListingViewModel(hotel, navigationService);
            viewModel.LoadReservationsCommand.Execute(null);
            return viewModel;
        }
        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _Reservations.Clear();  // Don't know why Sean does this, it's supposed to be empty at this point

            foreach (var res in reservations)
            {
                _Reservations.Add(new(res));
            }
        }
    }
}
