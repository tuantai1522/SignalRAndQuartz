using MediatR;

namespace Application.Sender.Commands.CreateSender
{
    public class CreateSenderCommand
        : IRequest<bool>
    {
        public string? SenderName { get; set; }
    }
}
