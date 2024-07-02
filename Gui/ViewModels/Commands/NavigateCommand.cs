using Gui.Stores;

namespace Gui.ViewModels.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly NavigationStore _NavigationStore;

        public NavigateCommand(NavigationStore navigationStore)
        {
            _NavigationStore = navigationStore;
        }

        public override void Execute(object? parameter)
        {
            _NavigationStore.CurrentViewModel = new MakeReservationViewModel(new Domain.Models.Hotel("Dummy"));
        }
    }
}
