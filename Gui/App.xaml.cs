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
            _NavigationStore.CurrentViewModel = ProvideReservationListViewModel();


            MainWindow = new MainWindow() { DataContext = new MainViewModel(_NavigationStore) };
            MainWindow.Show();

            //base.OnStartup(e);
        }
        private MakeReservationViewModel ProvideMakeReservationViewModel()
            => new MakeReservationViewModel(_Hotel, _NavigationStore, ProvideReservationListViewModel);

        private ReservationListingViewModel ProvideReservationListViewModel()
            => new ReservationListingViewModel(_Hotel, _NavigationStore, ProvideMakeReservationViewModel);

    }

}
