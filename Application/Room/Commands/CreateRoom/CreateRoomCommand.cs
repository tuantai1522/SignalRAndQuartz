using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Room.Commands.CreateRoom
{
    public class CreateRoomCommand
        : IRequest<bool>
    {
        public string? RoomName { get; set; }
    }
}
