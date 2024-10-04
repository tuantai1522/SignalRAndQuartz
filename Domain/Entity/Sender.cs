using MongoDB.Bson;

namespace Domain.Entity
{
    public class Sender
    {
        public ObjectId Id { get; set; }
        public string? SenderName { get; set; }
    }
}
