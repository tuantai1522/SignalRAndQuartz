using Domain.Interface;
using MediatR;

namespace Application.Message.Queries.GetListMessage
{
    public class GetListMessageQueryHandler
        : IRequestHandler<GetListMessageQuery, IList<Domain.Entity.Message>>

    {

        private readonly IMessageRepository _messageRepository;

        public GetListMessageQueryHandler(
            IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository
                ?? throw new ArgumentNullException(nameof(messageRepository));
        }

        public async Task<IList<Domain.Entity.Message>> Handle(GetListMessageQuery request, CancellationToken cancellationToken)
        {
            return await _messageRepository.GetAllMessages();
        }
    }
}
