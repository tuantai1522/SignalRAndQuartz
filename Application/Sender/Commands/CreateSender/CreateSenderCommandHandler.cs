using Domain.Interface;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Sender.Commands.CreateSender
{
    public class CreateSenderCommandHandler
        : IRequestHandler<CreateSenderCommand, bool>

    {
        private readonly ISenderRepository _senderRepository;

        public CreateSenderCommandHandler(
            ISenderRepository senderRepository)
        {
            _senderRepository = senderRepository
                ?? throw new ArgumentNullException(nameof(senderRepository));
        }
        public async Task<bool> Handle(CreateSenderCommand request, CancellationToken cancellationToken)
        {
            var sender = new Domain.Entity.Sender()
            {
                SenderName = request.SenderName,
            };

            await _senderRepository.CreateSender(sender);

            return true;
        }
    }
}
