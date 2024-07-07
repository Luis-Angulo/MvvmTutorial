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
        private Hotel _hotel;
        private HotelStore _hotelStore;
        private NavigationStore _navigationStore;
        private ReserveRoomDbContextFactory _dbContextFactory;

        public App()
        {
            _dbContextFactory = new ReserveRoomDbContextFactory(CONN_STRING);
            var resBook = new ReservationBook(
                new DatabaseReservationProvider(_dbContextFactory)
                , new DatabaseReservationCreator(_dbContextFactory)
                , new DatabaseReservationConflictValidator(_dbContextFactory)
                );
            _hotel = new("Casa de Marco", resBook);
            _hotelStore = new HotelStore(_hotel);
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            GenerateDbContext();
            _navigationStore.CurrentViewModel = ProvideReservationListViewModel();


            MainWindow = new MainWindow() { DataContext = new MainViewModel(_navigationStore) };
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
                _hotel
                , new(_navigationStore, ProvideReservationListViewModel)
                );

        private ReservationListingViewModel ProvideReservationListViewModel()
            => ReservationListingViewModel.LoadViewModel(
                _hotelStore
                , new(_navigationStore, ProvideMakeReservationViewModel)
                );

    }

}
