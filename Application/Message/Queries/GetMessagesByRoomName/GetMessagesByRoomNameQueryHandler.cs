using Application.Message.Queries.GetListMessage;
using Domain.Interface;
using MediatR;

namespace Application.Message.Queries.GetMessagesByRoomName
{
    public class GetMessagesByRoomNameQueryHandler
        : IRequestHandler<GetMessagesByRoomNameQuery, IList<Domain.Entity.Message>>

    {
        private readonly IMessageRepository _messageRepository;

        public GetMessagesByRoomNameQueryHandler(
            IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository
                ?? throw new ArgumentNullException(nameof(messageRepository));
        }

        public async Task<IList<Domain.Entity.Message>> Handle(GetMessagesByRoomNameQuery request, CancellationToken cancellationToken)
        {
            return await _messageRepository.GetMessagesByRoomName(request.RoomName);
        }
    }
}
