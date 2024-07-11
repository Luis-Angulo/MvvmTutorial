using Gui.Services;
using Gui.Stores;
using Gui.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gui.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(s =>
            {
                s.AddTransient<ReservationListingViewModel>(s => MakeReservationListingViewModel(s));
                s.AddSingleton((Func<IServiceProvider, Func<MakeReservationViewModel>>)((s) => () => s.GetRequiredService<MakeReservationViewModel>()));
                s.AddSingleton<NavigationService<ReservationListingViewModel>>();

                s.AddTransient<MakeReservationViewModel>();
                s.AddSingleton((Func<IServiceProvider, Func<ReservationListingViewModel>>)((s) => () => s.GetRequiredService<ReservationListingViewModel>()));
                s.AddSingleton<NavigationService<MakeReservationViewModel>>();

                s.AddSingleton<MainViewModel>();
            });

            return hostBuilder;
        }
        private static ReservationListingViewModel MakeReservationListingViewModel(IServiceProvider s)
        {
            return ReservationListingViewModel.LoadViewModel(
                s.GetRequiredService<HotelStore>()
                , s.GetRequiredService<NavigationService<MakeReservationViewModel>>()
                );
        }
    }
}
