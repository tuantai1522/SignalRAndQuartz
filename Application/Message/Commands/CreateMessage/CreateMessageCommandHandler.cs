using Domain.Entity;
using Domain.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Message.Commands.CreateMessage
{
    public class CreateMessageCommandHandler
        : IRequestHandler<CreateMessageCommand, Domain.Entity.Message>

    {
        private readonly IMessageRepository _messageRepository;

        public CreateMessageCommandHandler(
            IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository
                ?? throw new ArgumentNullException(nameof(messageRepository));
        }

        public async Task<Domain.Entity.Message> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new Domain.Entity.Message()
            {
                Content = request.Message,
                RoomName = request.RoomName,
                SenderName = request.UserName,
                FileData = request.File,
                FileName = request.FileName,
            };

            await _messageRepository.CreateMessage(message);

            return message;
        }
    }
}
