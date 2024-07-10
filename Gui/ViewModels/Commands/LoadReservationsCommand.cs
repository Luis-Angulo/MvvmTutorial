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
            _viewModel.IsLoading = true;
            _viewModel.ErrorMessage = string.Empty;
            try
            {
                await _hotelStore.Load();
                // throw new Exception("");  // Manually throw exception to test error message on ViewModels
                _viewModel.UpdateReservations(_hotelStore.Reservations);
            } catch (Exception ex)
            {
                _viewModel.ErrorMessage = "Failed to load reservations";
            }
            _viewModel.IsLoading = false;
        }
    }
}
