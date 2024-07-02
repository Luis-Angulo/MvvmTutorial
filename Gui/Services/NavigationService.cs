using Gui.Stores;
using Gui.ViewModels.Abstractions;

namespace Gui.Services
{
    public class NavigationService
    {
        private readonly NavigationStore _NavigationStore;
        private readonly Func<ViewModelBase> _ViewModelProvider;

        public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> viewModelProvider)
        {
            _NavigationStore = navigationStore;
            _ViewModelProvider = viewModelProvider;
        }
        public void Navigate()
        {
            _NavigationStore.CurrentViewModel = _ViewModelProvider();
        }
    }
}
