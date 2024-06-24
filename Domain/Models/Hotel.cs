namespace Domain.Models
{
    public class Hotel
    {
        private readonly ReservationBook _ReservationBook;
        public string Name { get; }
        public Hotel(string name)
        {
            _ReservationBook = new();
            Name = name;
        }
    }
}
