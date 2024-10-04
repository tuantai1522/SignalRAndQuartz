using Application.Message.Queries.GetListMessage;
using Domain.Interface;
using Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Message.Queries.GetMessageById
{
    public class GetMessageByIdQueryHandler
        : IRequestHandler<GetMessageByIdQuery, Domain.Entity.Message>

    {

        private readonly IMessageRepository _messageRepository;

        public GetMessageByIdQueryHandler(
            IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository
                ?? throw new ArgumentNullException(nameof(messageRepository));
        }
        public async Task<Domain.Entity.Message> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
            => await _messageRepository.GetMessageById(request.MessageId);

    }
}
