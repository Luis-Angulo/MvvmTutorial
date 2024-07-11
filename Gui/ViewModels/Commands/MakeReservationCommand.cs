using Domain.Exceptions;
using Domain.Models;
using Gui.Services;
using Gui.Stores;
using System.ComponentModel;
using System.Windows;

namespace Gui.ViewModels.Commands
{
    public class MakeReservationCommand : AsyncCommandBase
    {
        private readonly HotelStore _hotelStore;
        private readonly MakeReservationViewModel _vm;
        private readonly NavigationService<ReservationListingViewModel> _navigationService;

        public MakeReservationCommand(
            HotelStore hotelStore
            , MakeReservationViewModel vm
            , NavigationService<ReservationListingViewModel> navigationService)
        {
            _hotelStore = hotelStore;
            _vm = vm;
            _navigationService = navigationService;
            _vm.PropertyChanged += OnViewModelPropertyChanged;
        }
        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_vm.UserName)
                && _vm.FloorNumber > 0
                && base.CanExecute(parameter);
        }
        public async override Task ExecuteAsync(object? parameter)
        {
            var res = new Reservation(
                _vm.UserName
                , new RoomId(_vm.FloorNumber, _vm.RoomNumber)
                , _vm.StartDate
                , _vm.EndDate
                );
            try
            {
                await _hotelStore.MakeReservation(res);

                MessageBox.Show(
                    "Reservation created"
                    , "Success"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Information
                    );

                _navigationService.Navigate();
            }
            catch (ReservationConflictException)
            {
                MessageBox.Show(
                    "This room is already taken"
                    , "Error"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Error
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to make reservation"
                    , "Error"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Error
                    );
            }
        }
        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (
                (e.PropertyName == nameof(MakeReservationViewModel.UserName))
                || (e.PropertyName == nameof(MakeReservationViewModel.FloorNumber)))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
