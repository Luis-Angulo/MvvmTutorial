using Domain.Exceptions;
using Domain.Models;
using Gui.Services;
using System.ComponentModel;
using System.Windows;

namespace Gui.ViewModels.Commands
{
    public class MakeReservationCommand : CommandBase
    {
        private readonly Hotel _Hotel;
        private readonly MakeReservationViewModel _Vm;
        private readonly NavigationService _NavigationService;

        public MakeReservationCommand(Hotel hotel, MakeReservationViewModel vm, NavigationService navigationService)
        {
            _Hotel = hotel;
            _Vm = vm;
            _NavigationService = navigationService;
            _Vm.PropertyChanged += OnViewModelPropertyChanged;
        }
        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_Vm.UserName)
                && _Vm.FloorNumber > 0
                && base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            var res = new Reservation(
                _Vm.UserName
                , new RoomId(_Vm.FloorNumber, _Vm.RoomNumber)
                , _Vm.StartDate
                , _Vm.EndDate
                );
            try
            {
                _Hotel.MakeReservation(res);
                MessageBox.Show(
                    "Reservation created"
                    , "Success"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Information
                    );
                _NavigationService.Navigate();
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
