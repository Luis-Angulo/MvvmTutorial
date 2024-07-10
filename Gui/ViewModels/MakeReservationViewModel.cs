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
                var propName = nameof(StartDate);  // Friedlier to refactors than HC string
                ClearErrors(propName);  // Clear previous errors
                ClearErrors(nameof(EndDate));
                if (EndDate < StartDate)
                {
                    var errorMessage = "Reservation StartDate cannot be after EndDate";
                    AddError(errorMessage, propName);
                }
                OnPropertyChanged(propName);
            }
        }
        private DateTime _EndDate;
        public DateTime EndDate
        {
            get => _EndDate;
            set
            {
                _EndDate = value;
                var propName = nameof(EndDate);  // Friedlier to refactors than HC string
                ClearErrors(propName);  // Clear previous errors
                ClearErrors(nameof(StartDate));
                if (EndDate < StartDate)
                {
                    var errorMessage = "Reservation EndDate cannot be before StartDate";
                    AddError(errorMessage, propName);
                }
                OnPropertyChanged(propName);
            }
        }

        private void AddError(string errorMessage, string propertyName)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>() { });
            }
            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);
            OnErrorsChanged(nameof(EndDate));
        }

        private void OnErrorsChanged(string propertyName)
        {
            // Implementing INotifyDataErrorInfo allows hooking into error handling built into the WPF component model
            // This fires off an event that notifies the component model that there's a bad data error in a UserControl component
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);
            OnErrorsChanged(propertyName);
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
