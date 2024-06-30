using Domain.Models;
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
                OnPropertyChanged(nameof(_UserName));
            }
        }
        private int _FloorNumber;
        public int FloorNumber
        {
            get => _FloorNumber;
            set
            {
                _FloorNumber = value;
                OnPropertyChanged(nameof(_FloorNumber));
            }
        }
        private int _RoomNumber;
        public int RoomNumber
        {
            get => _RoomNumber;
            set
            {
                _RoomNumber = value;
                OnPropertyChanged(nameof(_RoomNumber));
            }
        }
        private DateTime _StartDate;
        public DateTime StartDate
        {
            get => _StartDate;
            set
            {
                _StartDate = value;
                OnPropertyChanged(nameof(_StartDate));
            }
        }
        private DateTime _EndDate;
        public DateTime EndDate
        {
            get => _EndDate;
            set
            {
                _EndDate = value;
                OnPropertyChanged(nameof(_EndDate));
            }
        }
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public MakeReservationViewModel(Hotel hotel)
        {   
            SubmitCommand = new MakeReservationCommand(hotel, this);
            CancelCommand = new CancelReservationCommand();
        }
    }
}
