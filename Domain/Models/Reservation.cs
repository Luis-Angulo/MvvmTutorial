namespace Domain.Models
{
    public class Reservation
    {
        public string UserName { get; }
        public RoomId RoomId { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public TimeSpan Length => EndTime.Subtract(StartTime);
        public Reservation(string userName, RoomId roomId, DateTime startTime, DateTime endTime)
        {
            UserName = userName;
            RoomId = roomId;
            StartTime = startTime;
            EndTime = endTime;
        }

        public bool Conflicts(Reservation reservation)
        {
            // Different rooms cannot conflict, no matter the time of the reservation of each
            // TODO: Don't like this but following the tutorial, may remove later to use .Equals, see RoomId code
            if (!reservation.RoomId.Equals(RoomId))
            {
                return false;
            }
            return reservation.StartTime < EndTime && reservation.EndTime > StartTime;
        }
    }
}
