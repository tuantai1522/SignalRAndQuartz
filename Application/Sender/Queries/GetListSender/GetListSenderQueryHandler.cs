using Application.Message.Queries.GetListMessage;
using Domain.Interface;
using Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sender.Queries.GetListSender
{
    public class GetListSenderQueryHandler
        : IRequestHandler<GetListSenderQuery, IList<Domain.Entity.Sender>>

    {

        private readonly ISenderRepository _senderRepository;

        public GetListSenderQueryHandler(
            ISenderRepository senderRepository)
        {
            _senderRepository = senderRepository
                ?? throw new ArgumentNullException(nameof(senderRepository));
        }
        public async Task<IList<Domain.Entity.Sender>> Handle(GetListSenderQuery request, CancellationToken cancellationToken)
        {
            return await _senderRepository.GetAllSenders();

        }
    }
}
