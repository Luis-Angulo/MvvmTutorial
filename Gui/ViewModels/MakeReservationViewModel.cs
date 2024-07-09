using Gui.Services;
using Gui.Stores;
using Gui.ViewModels.Abstractions;
using Gui.ViewModels.Commands;
using System.Collections;
using System.ComponentModel;
using System.Windows.Input;

namespace Gui.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private string? _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
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
                _propertyNameToErrorsDictionary.Remove(nameof(EndDate));  // Clear previous errors
                if(EndDate < StartDate)
                {
                    var endDateErrors = new List<string>() { 
                        "Reservation EndDate cannot be prior than StartDate"
                    };
                    _propertyNameToErrorsDictionary.Add(nameof(EndDate), endDateErrors);
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(EndDate)));
                }
                OnPropertyChanged(nameof(EndDate));
            }
        }
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        public MakeReservationViewModel(HotelStore hotelStore, NavigationService navigationService)
        {
            SubmitCommand = new MakeReservationCommand(hotelStore, this, navigationService);
            CancelCommand = new NavigateCommand(navigationService);

            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
            _StartDate = DateTime.Now;
            _EndDate = DateTime.Now.AddDays(1);
        }
        #region UI Error handling
        private Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();
        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyNameToErrorsDictionary
                .GetValueOrDefault(propertyName, new List<string>());
        } 
        #endregion
    }
}
