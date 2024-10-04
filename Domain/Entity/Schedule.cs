using Domain.Entity;
using MongoDB.Bson;

namespace Domain.Entity
{
    public class Schedule
    {
        public ObjectId Id { get; set; }
        public string? Name { get; set; }
        public DateTime LastExecutedDate { get; set; }
        public long? SecondsToExecute { get; set; }
        public string? Type { get; set; }
        public IList<string> RoomNames { get; set; } = [];
        public bool IsActive { get; set; }
    }
}
