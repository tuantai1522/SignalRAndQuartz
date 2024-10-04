using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Message.Commands.CreateMessage
{
    public class CreateMessageCommand
        : IRequest<Domain.Entity.Message>
    {
        public string? UserName { get; set; }
        public string? RoomName { get; set; }
        public string? Message { get; set; }
        public byte[]? File { get; set; }
        public string? FileName { get; set; }

    }
}
