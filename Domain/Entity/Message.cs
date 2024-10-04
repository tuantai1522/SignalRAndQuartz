using MongoDB.Bson;

namespace Domain.Entity
{
    public class Message
    {
        public ObjectId Id { get; set; }
        public string? SenderName { get; set; }
        public string? RoomName { get; set; }
        public string? Content { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public byte[]? FileData { get; set; } // Thêm thuộc tính để lưu file nhị phân
        public string? FileName { get; set; } // Thêm thuộc tính để lưu file nhị phân

    }
}
