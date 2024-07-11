using Gui.Stores;
using Gui.ViewModels.Abstractions;

namespace Gui.Services
{
    public class NavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _NavigationStore;
        private readonly Func<TViewModel> _ViewModelProvider;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> viewModelProvider)
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
