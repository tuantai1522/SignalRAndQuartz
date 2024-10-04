using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Room.Queries.GetRoomByRoomName
{
    public class GetRoomByRoomNameQuery
        : IRequest<Domain.Entity.Room>

    {
        public string? RoomName { get; set; }

    }
}
