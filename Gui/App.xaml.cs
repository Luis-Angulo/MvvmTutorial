using Domain.Models;

using System.Windows;

namespace Gui
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //var hotel = new Hotel("Casa Marco");
            //try
            //{
            //    hotel.MakeReservation(new Reservation(
            //        "SingletonSean"
            //        ,new RoomId(1,3)
            //        ,new DateTime(2000, 1, 1)
            //        ,new DateTime(2000, 1, 2)
            //    ));
            //    hotel.MakeReservation(new Reservation(
            //        "SingletonSean"
            //        , new RoomId(1, 3)
            //        , new DateTime(2000, 1, 1)
            //        , new DateTime(2000, 1, 4)
            //    ));
            //} catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            //var reservations = hotel.GetReservationsByUserName("SingletonSean");

            base.OnStartup(e);
        }
    }

}
