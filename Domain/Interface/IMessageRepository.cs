using Domain.Entity;

namespace Domain.Interface
{
    public interface IMessageRepository
    {
        Task<IList<Message>> GetAllMessages();
        Task<IList<Message>> GetMessagesByRoomName(string? RoomName);
        Task<Message> GetMessageById(string? messageId);
        Task<Message> CreateMessage(Message message);
    }
}
