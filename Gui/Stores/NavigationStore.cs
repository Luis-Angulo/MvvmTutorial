using Gui.ViewModels.Abstractions;

namespace Gui.Stores
{
    public class NavigationStore
    {
        private ViewModelBase _CurrentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set { _CurrentViewModel = value; }
        }
    }
}
