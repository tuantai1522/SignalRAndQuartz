using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sender.Queries.GetListSender
{
    public class GetListSenderQuery
        : IRequest<IList<Domain.Entity.Sender>>
    {
    }
}
