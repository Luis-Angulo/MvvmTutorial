using Domain.Models;
using Gui.Stores;
using Gui.ViewModels;
using System.Windows;

namespace Gui
{
    public partial class App : Application
    {
        private Hotel _Hotel;
        private NavigationStore _NavigationStore;

        public App()
        {
            _Hotel = new("Casa de Marco");
            _NavigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _NavigationStore.CurrentViewModel = new ReservationListingViewModel(_NavigationStore);
            MainWindow = new MainWindow() { DataContext = new MainViewModel(_NavigationStore) };
            MainWindow.Show();

            //base.OnStartup(e);
        }
    }

}
