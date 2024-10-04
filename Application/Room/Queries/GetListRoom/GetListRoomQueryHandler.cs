using Application.Message.Queries.GetListMessage;
using Domain.Interface;
using Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Room.Queries.GetListRoom
{
    public class GetListRoomQueryHandler
        : IRequestHandler<GetListRoomQuery, IList<Domain.Entity.Room>>

    {
        private readonly IRoomRepository _roomRepository;

        public GetListRoomQueryHandler(
            IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository
                ?? throw new ArgumentNullException(nameof(roomRepository));
        }

        public async Task<IList<Domain.Entity.Room>> Handle(GetListRoomQuery request, CancellationToken cancellationToken)
            => await _roomRepository.GetRooms();
    }
}
