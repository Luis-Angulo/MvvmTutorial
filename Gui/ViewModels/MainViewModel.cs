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
            _NavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;  // Add delegate to trigger WPF repaint
        }
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
