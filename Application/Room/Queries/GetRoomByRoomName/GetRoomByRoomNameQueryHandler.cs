using Application.Sender.Queries.GetSenderByUserName;
using Domain.Interface;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Room.Queries.GetRoomByRoomName
{
    public class GetRoomByRoomNameQueryHandler
        : IRequestHandler<GetRoomByRoomNameQuery, Domain.Entity.Room>

    {
        private readonly IRoomRepository _roomRepository;

        public GetRoomByRoomNameQueryHandler(
            IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository
                ?? throw new ArgumentNullException(nameof(roomRepository));
        }

        public async Task<Domain.Entity.Room> Handle(GetRoomByRoomNameQuery request, CancellationToken cancellationToken)
        {
            return await _roomRepository.GetRoomByRoomName(request.RoomName);
        }
    }
}
