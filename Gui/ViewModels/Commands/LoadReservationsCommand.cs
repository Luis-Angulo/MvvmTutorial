
using Domain.Models;
using System.Windows;

namespace Gui.ViewModels.Commands
{
    public class LoadReservationsCommand : AsyncCommandBase
    {
        private readonly Hotel _hotel;
        private readonly ReservationListingViewModel _viewModel;

        public LoadReservationsCommand(Hotel hotel, ReservationListingViewModel viewModel)
        {
            _hotel = hotel;
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            
            try
            {
                IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();
                _viewModel.UpdateReservations(reservations);
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
