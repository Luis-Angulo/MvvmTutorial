using Domain.Models;

namespace Gui.ViewModels.Commands
{
    public class MakeReservationCommand : CommandBase
    {
        private readonly Hotel _Hotel;
        private readonly MakeReservationViewModel _Vm;
        public MakeReservationCommand(Hotel hotel, MakeReservationViewModel vm)
        {
            _Hotel = hotel;
            _Vm = vm;
        }
        public override void Execute(object? parameter)
        {
            var res = new Reservation(
                _Vm.UserName
                , new RoomId(_Vm.FloorNumber, _Vm.RoomNumber)
                , _Vm.StartDate
                , _Vm.EndDate
                );
            _Hotel.MakeReservation(res);
        }
    }
}
