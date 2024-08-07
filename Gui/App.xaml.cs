﻿using Domain.Models;
using Gui.DbContexts;
using Gui.HostBuilders;
using Gui.Services;
using Gui.Services.ReservationConflictValidators;
using Gui.Services.ReservationCreators;
using Gui.Services.ReservationProviders;
using Gui.Stores;
using Gui.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Gui
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            // Subbing bespoke instanciation of dependencies for DI using the generic Host is just moving everything to the host and then asking it for instances
            _host = Host.CreateDefaultBuilder()
                .AddViewModels()  // Moved ViewModel setup to extension method
                .ConfigureServices((ctx, s) =>
                {
                    var connectionString = ctx.Configuration.GetConnectionString("Default");
                    var hotelName = ctx.Configuration.GetValue<string>("HotelName");

                    s.AddSingleton(new ReserveRoomDbContextFactory(connectionString));
                    s.AddSingleton<IReservationProvider, DatabaseReservationProvider>();
                    s.AddSingleton<IReservationCreator, DatabaseReservationCreator>();
                    s.AddSingleton<IReservationConflictValidator, DatabaseReservationConflictValidator>();

                    s.AddTransient<ReservationBook>();  // Because in theory a Hotel could have different ReservationBook instances
                    s.AddSingleton(s => new Hotel(hotelName, s.GetRequiredService<ReservationBook>()));  // FactoryFunc to pass in string hotelName

                    s.AddSingleton<HotelStore>();
                    s.AddSingleton<NavigationStore>();

                    s.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    });
                }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            GenerateDbContext(_host.Services.GetRequiredService<ReserveRoomDbContextFactory>());
            var navigationService = _host.Services.GetRequiredService<NavigationService<ReservationListingViewModel>>();
            navigationService.Navigate();

            // TODO: Implement DI using host, follow https://www.youtube.com/watch?v=dgJ1nS2CLpQ @ 7:30, pending refactor of NavigationService to use Generic types
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            //base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();  // shut down host on exit
            base.OnExit(e);
        }

        private void GenerateDbContext(ReserveRoomDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }
        }
    }

}
