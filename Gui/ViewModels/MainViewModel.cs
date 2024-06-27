using Gui.ViewModels.Abstractions;

namespace Gui.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; }
        public MainViewModel()
        {
            CurrentViewModel = new ReservationListingViewModel();
        }
    }
}
