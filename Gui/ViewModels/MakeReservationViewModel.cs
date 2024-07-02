using Domain.Models;
using Gui.Services;
using Gui.Stores;
using Gui.ViewModels.Abstractions;
using Gui.ViewModels.Commands;
using System.Windows.Input;

namespace Gui.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase
    {
        private string? _UserName;
        public string UserName
        {
            get => _UserName;
            set
            {
                _UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        private int _FloorNumber;
        public int FloorNumber
        {
            get => _FloorNumber;
            set
            {
                _FloorNumber = value;
                OnPropertyChanged(nameof(FloorNumber));
            }
        }
        private int _RoomNumber;
        public int RoomNumber
        {
            get => _RoomNumber;
            set
            {
                _RoomNumber = value;
                OnPropertyChanged(nameof(RoomNumber));
            }
        }
        private DateTime _StartDate;
        public DateTime StartDate
        {
            get => _StartDate;
            set
            {
                _StartDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        private DateTime _EndDate;
        public DateTime EndDate
        {
            get => _EndDate;
            set
            {
                _EndDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public MakeReservationViewModel(Hotel hotel, NavigationService navigationService)
        {   
            SubmitCommand = new MakeReservationCommand(hotel, this, navigationService);
            CancelCommand = new NavigateCommand(navigationService);
            _StartDate = DateTime.Now;
            _EndDate = DateTime.Now.AddDays(1);
        }
    }
}
