using System.Windows.Input;

namespace Gui.ViewModels.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }
        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
        public abstract void Execute(object? parameter);
    }
}
