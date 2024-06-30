using Domain.Models;
using Gui.ViewModels;
using System.Windows;

namespace Gui
{
    public partial class App : Application
    {
        private Hotel _Hotel;

        public App()
        {
            _Hotel = new("Casa de Marco");
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow() { DataContext = new MainViewModel(_Hotel) };
            MainWindow.Show();

            //base.OnStartup(e);
        }
    }

}
