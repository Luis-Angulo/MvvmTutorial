using Domain.Models;
using Gui.Services;
using Gui.Stores;
using Gui.ViewModels.Abstractions;
using Gui.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Gui.ViewModels
{
    public class ReservationListingViewModel : ViewModelBase
    {
        private ObservableCollection<ReservationViewModel> _reservations;
        public IEnumerable<ReservationViewModel> Reservations => _reservations;
        public ICommand MakeReservationCommand { get; }
        public ICommand LoadReservationsCommand { get; }
        // UNCOMMENT
        //private ReservationListingViewModel(HotelStore hotelStore, NavigationService navigationService)
        //{
        //    _reservations = new ObservableCollection<ReservationViewModel>();

        //    MakeReservationCommand = new NavigateCommand(navigationService);
        //    LoadReservationsCommand = new LoadReservationsCommand(hotelStore, this);
        //}
        //public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore, NavigationService navigationService)
        //{
        //    var viewModel = new ReservationListingViewModel(hotelStore, navigationService);
        //    viewModel.LoadReservationsCommand.Execute(null);
        //    return viewModel;
        //}
        public MakeReservationViewModel MakeReservationViewModel { get; } // TEMP
        private ReservationListingViewModel(HotelStore hotelStore, NavigationService navigationService, MakeReservationViewModel makeReservationViewModel) // TEMP
        {
            _reservations = new ObservableCollection<ReservationViewModel>();
            MakeReservationViewModel = makeReservationViewModel;

            MakeReservationCommand = new NavigateCommand(navigationService);
            LoadReservationsCommand = new LoadReservationsCommand(hotelStore, this);

            hotelStore.ReservationMade += OnReservationMade;
        }

        private void OnReservationMade(Reservation reservation)
        {
            _reservations.Add(new ReservationViewModel(reservation));
        }

        public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore, NavigationService navigationService, MakeReservationViewModel makeReservationViewModel) // TEMP
        {
            var viewModel = new ReservationListingViewModel(hotelStore, navigationService, makeReservationViewModel);
            viewModel.LoadReservationsCommand.Execute(null);
            return viewModel;
        }
        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _reservations.Clear();  // Don't know why Sean does this, it's supposed to be empty at this point

            foreach (var res in reservations)
            {
                _reservations.Add(new(res));
            }
        }
    }
}
