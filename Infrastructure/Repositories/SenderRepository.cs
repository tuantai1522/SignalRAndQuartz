using Domain.Entity;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SenderRepository : ISenderRepository
    {
        private readonly ChatDbContext _context;

        public SenderRepository(ChatDbContext context)
        {
            _context = context;
        }
        public async Task CreateSender(Sender sender)
        {
            _context.Senders.Add(sender);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Sender>> GetAllSenders()
            => await _context.Senders.AsNoTracking().ToListAsync();

        public async Task<Sender> GetSenderByUserName(string userName)
            => await _context.Senders.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.SenderName.Equals(userName, StringComparison.OrdinalIgnoreCase));

    }
}
