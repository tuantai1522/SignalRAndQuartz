using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Room.Queries.GetListRoom
{
    public class GetListRoomQuery
        : IRequest<IList<Domain.Entity.Room>>
    {
    }
}
