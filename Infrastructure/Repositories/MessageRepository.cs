using Domain.Entity;
using Domain.Interface;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext _context;

        public MessageRepository(ChatDbContext context)
        {
            _context = context;
        }
        public async Task<Message> CreateMessage(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return message;
        }

        public async Task<IList<Message>> GetAllMessages()
            => await _context.Messages.AsNoTracking().ToListAsync();

        public async Task<Message> GetMessageById(string? messageId)
            => await _context.Messages
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id.ToString().Equals(messageId));

        public async Task<IList<Message>> GetMessagesByRoomName(string? RoomName)
            => await _context.Messages
                    .AsNoTracking()
                    .Where(x => x.RoomName == RoomName)
                    .ToListAsync();

    }
}
