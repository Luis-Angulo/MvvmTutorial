using Gui.Services;

namespace Gui.ViewModels.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly NavigationService _NavigationService;
        public NavigateCommand(NavigationService navigationService)
        {
            _NavigationService = navigationService;
        }
        public override void Execute(object? parameter)
        {
            _NavigationService.Navigate();
        }
    }
}
