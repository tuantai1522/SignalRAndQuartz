using Domain.Entity;

namespace Domain.Interface
{
    public interface IRoomRepository
    {
        Task<IList<Room>> GetRooms();
        Task<Room> GetRoomByRoomName(string roomName);
        Task CreateRoom(Room room);
    }
}
