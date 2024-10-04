using Application.Message.Commands.CreateMessage;
using Domain.Interface;
using Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Room.Commands.CreateRoom
{
    public class CreateRoomCommandHandler
        : IRequestHandler<CreateRoomCommand, bool>

    {
        private readonly IRoomRepository _roomRepository;

        public CreateRoomCommandHandler(
            IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository
                ?? throw new ArgumentNullException(nameof(roomRepository));
        }
        public async Task<bool> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = new Domain.Entity.Room()
            {
                RoomName = request.RoomName,
            };

            await _roomRepository.CreateRoom(room);

            return true;
        }
    }
}
