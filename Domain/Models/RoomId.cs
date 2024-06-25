namespace Domain.Models
{
    public class RoomId
    {
        public int FloorNumber { get; }
        public int RoomNumber { get; }
        // TODO: Don't like this but following the tutorial
        public override string ToString()
        {
            return $"{FloorNumber},{RoomNumber}";
        }
        public override bool Equals(object? obj)
        {
            return obj is RoomId roomId 
                && FloorNumber == roomId.FloorNumber
                && RoomNumber == roomId.RoomNumber;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(FloorNumber, RoomNumber);
        }
        // Not overriding the == operator like in the tutorial, don't wanna do that for obj comparison
    }
}
