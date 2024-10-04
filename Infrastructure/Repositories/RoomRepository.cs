using Domain.Entity;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ChatDbContext _context;

        public RoomRepository(ChatDbContext context)
        {
            _context = context;
        }
        public async Task CreateRoom(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task<Room> GetRoomByRoomName(string roomName)
            => await _context.Rooms
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.RoomName == roomName);

        public async Task<IList<Room>> GetRooms()
            => await _context.Rooms.AsNoTracking().ToListAsync();
    }
}
