using Gui.Stores;
using Gui.ViewModels.Abstractions;

namespace Gui.ViewModels.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly NavigationStore _NavigationStore;
        private readonly Func<ViewModelBase> _ViewModelProvider;

        public NavigateCommand(NavigationStore navigationStore, Func<ViewModelBase> viewModelProvider)
        {
            _NavigationStore = navigationStore;
            _ViewModelProvider = viewModelProvider;
        }

        public override void Execute(object? parameter)
        {
            _NavigationStore.CurrentViewModel = _ViewModelProvider();
        }
    }
}
