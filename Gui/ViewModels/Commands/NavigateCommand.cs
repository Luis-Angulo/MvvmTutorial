using Gui.Services;
using Gui.ViewModels.Abstractions;

namespace Gui.ViewModels.Commands
{
    public class NavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly NavigationService<TViewModel> _NavigationService;
        public NavigateCommand(NavigationService<TViewModel> navigationService)
        {
            _NavigationService = navigationService;
        }
        public override void Execute(object? parameter)
        {
            _NavigationService.Navigate();
        }
    }
}
