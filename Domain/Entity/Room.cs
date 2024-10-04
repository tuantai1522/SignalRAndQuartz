using MongoDB.Bson;

namespace Domain.Entity
{
    public class Room
    {
        public ObjectId Id { get; set; }
        public string? RoomName { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
