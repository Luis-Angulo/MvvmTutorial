using Domain.Models;
using Gui.DbContexts;
using Gui.Services.ReservationConflictValidators;
using Gui.Services.ReservationCreators;
using Gui.Services.ReservationProviders;
using Gui.Stores;
using Gui.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace Gui
{
    public partial class App : Application
    {
        private const string CONN_STRING = "Data Source=reserveroom.db";
        private Hotel _Hotel;
        private NavigationStore _NavigationStore;
        private ReserveRoomDbContextFactory _dbContextFactory;

        public App()
        {
            _dbContextFactory = new ReserveRoomDbContextFactory(CONN_STRING);
            var resBook = new ReservationBook(
                new DatabaseReservationProvider(_dbContextFactory)
                , new DatabaseReservationCreator(_dbContextFactory)
                , new DatabaseReservationConflictValidator(_dbContextFactory)
                );
            _Hotel = new("Casa de Marco", resBook);
            _NavigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            GenerateDbContext();
            _NavigationStore.CurrentViewModel = ProvideReservationListViewModel();


            MainWindow = new MainWindow() { DataContext = new MainViewModel(_NavigationStore) };
            MainWindow.Show();

            //base.OnStartup(e);
        }

        private void GenerateDbContext()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }
        }

        private MakeReservationViewModel ProvideMakeReservationViewModel()
            => new MakeReservationViewModel(
                _Hotel
                , new(_NavigationStore, ProvideReservationListViewModel)
                );

        private ReservationListingViewModel ProvideReservationListViewModel()
            => new ReservationListingViewModel(
                _Hotel
                , new(_NavigationStore, ProvideMakeReservationViewModel)
                );

    }

}
