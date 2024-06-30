﻿using Gui.ViewModels.Abstractions;
using Domain.Models;

namespace Gui.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; }
        public MainViewModel()
        {
            CurrentViewModel = new MakeReservationViewModel();
        }
    }
}
