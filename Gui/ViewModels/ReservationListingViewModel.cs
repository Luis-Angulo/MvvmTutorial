﻿using Domain.Models;
using Gui.Stores;
using Gui.ViewModels.Abstractions;
using Gui.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Gui.ViewModels
{
    public class ReservationListingViewModel : ViewModelBase
    {
        private ObservableCollection<ReservationViewModel> _Reservations;
        public IEnumerable<ReservationViewModel> Reservations => _Reservations;
        public ICommand MakeReservationCommand { get; }
        private Hotel _Hotel { get; }
        public ReservationListingViewModel(Hotel hotel, NavigationStore navStore, Func<MakeReservationViewModel> viewModelProvider)
        {
            _Reservations = new ObservableCollection<ReservationViewModel>();
            MakeReservationCommand = new NavigateCommand(navStore, viewModelProvider);
            _Hotel = hotel;

            UpdateReservations();
        }
        private void UpdateReservations()
        {
            _Reservations.Clear();  // Don't know why Sean does this, it's supposed to be empty at this point

            foreach (var res in _Hotel.GetAllReservations())
            {
                _Reservations.Add(new(res));
            }
        }
    }
}
