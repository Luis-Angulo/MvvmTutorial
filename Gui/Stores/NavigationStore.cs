using Gui.ViewModels.Abstractions;

namespace Gui.Stores
{
    public class NavigationStore
    {
        private ViewModelBase? _CurrentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set { 
                _CurrentViewModel = value;
                OnCurrentViewModelChange();
            }
        }
        public event Action? CurrentViewModelChanged;
        private void OnCurrentViewModelChange()
        {
            CurrentViewModelChanged?.Invoke();  // Invoke delegates to "hook" into WPF repaint lifecycle
        }
    }
}
