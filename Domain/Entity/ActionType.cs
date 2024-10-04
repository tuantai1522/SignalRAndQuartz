using MongoDB.Bson;

namespace Domain.Entity
{
    public class ActionType
    {
        public ObjectId Id { get; set; }
        public string? TypeName { get; set; }
    }
}
