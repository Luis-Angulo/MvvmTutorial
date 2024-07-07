using Gui.Stores;
using System.Windows;

namespace Gui.ViewModels.Commands
{
    public class LoadReservationsCommand : AsyncCommandBase
    {
        private readonly HotelStore _hotelStore;
        private readonly ReservationListingViewModel _viewModel;

        public LoadReservationsCommand(HotelStore hotelStore, ReservationListingViewModel viewModel)
        {
            _hotelStore = hotelStore;
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            
            try
            {
                await _hotelStore.Load();
                _viewModel.UpdateReservations(_hotelStore.Reservations);
            } catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load reservations"
                    , "Error"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Error
                    );
            }
        }
    }
}
