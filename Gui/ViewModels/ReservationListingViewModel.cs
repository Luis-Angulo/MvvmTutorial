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
        private readonly HotelStore _hotelStore;
        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { 
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }
        public bool HasErrorMessage => !string.IsNullOrEmpty(_errorMessage);
        public IEnumerable<ReservationViewModel> Reservations => _reservations;
        public ICommand MakeReservationCommand { get; }
        public ICommand LoadReservationsCommand { get; }
        private bool _isLoading;
        public bool IsLoading  // Controls loading spinner animation
        {
            get { return _isLoading; }
            set { 
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        private ReservationListingViewModel(HotelStore hotelStore, NavigationService navigationService)
        {
            _hotelStore = hotelStore;
            _hotelStore.ReservationMade += OnReservationMade;
            _reservations = new ObservableCollection<ReservationViewModel>();

            MakeReservationCommand = new NavigateCommand(navigationService);
            LoadReservationsCommand = new LoadReservationsCommand(hotelStore, this);
        }        
        public override void Dispose()
        {
            _hotelStore.ReservationMade -= OnReservationMade;  // Prevents memory leaks
            base.Dispose();
        }
        private void OnReservationMade(Reservation reservation)
        {
            _reservations.Add(new ReservationViewModel(reservation));
        }
        public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore, NavigationService navigationService)
        {
            var viewModel = new ReservationListingViewModel(hotelStore, navigationService);
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
