﻿using Domain.Models;

namespace Gui.Stores
{
    // HotelStore works as a cache of Reservation data to prevent excessive requerying of the database
    public class HotelStore
    {
        private readonly List<Reservation> _reservations;
        private readonly Hotel _hotel;
        private Lazy<Task> _initializeLazy;
        public event Action<Reservation>? ReservationMade;
        public IEnumerable<Reservation> Reservations => _reservations;
        public HotelStore(Hotel hotel)
        {
            _reservations = new List<Reservation>();
            _initializeLazy = new Lazy<Task>(Initialize);
            _hotel = hotel;
        }
        public async Task Load()
        {
            try
            {
                await _initializeLazy.Value;
            }
            catch (Exception)
            {
                _initializeLazy = new Lazy<Task>(Initialize);
                throw;
            }
        }
        /// <summary>
        /// Adds a reservation to the backing database and to the backing in memory store
        /// </summary>
        /// <param name="res">new reservation to add</param>
        /// <returns>Task</returns>
        public async Task MakeReservation(Reservation res)
        {
            await _hotel.MakeReservation(res);
            _reservations.Add(res);
            OnReservationMade(res);
        }
        private void OnReservationMade(Reservation res)
        {
            ReservationMade?.Invoke(res);
        }
        private async Task Initialize()
        {
            IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();

            _reservations.Clear();
            _reservations.AddRange(reservations);
        }
    }
}
