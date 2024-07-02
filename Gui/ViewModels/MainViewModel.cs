using Gui.ViewModels.Abstractions;
using Domain.Models;
using Gui.Stores;

namespace Gui.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private NavigationStore _NavigationStore;
        public ViewModelBase CurrentViewModel => _NavigationStore.CurrentViewModel;
        public MainViewModel(NavigationStore navigationStore)
        {
            _NavigationStore = navigationStore;
        }
    }
}
