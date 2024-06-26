﻿namespace Domain.Models
{
    public class Hotel
    {
        private readonly ReservationBook _ReservationBook;
        public string Name { get; }
        public Hotel(string name)
        {
            Name = name;
            _ReservationBook = new();
        }
        public IEnumerable<Reservation> GetAllReservations()
            => _ReservationBook.GetAllReservations();

        public void MakeReservation(Reservation reservation)
            => _ReservationBook.AddReservation(reservation);

    }
}
