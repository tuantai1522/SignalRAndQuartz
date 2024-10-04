using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Message.Queries.GetMessageById
{
    public class GetMessageByIdQuery
        : IRequest<Domain.Entity.Message>

    {
        public string? MessageId { get; set; }
    }
}
