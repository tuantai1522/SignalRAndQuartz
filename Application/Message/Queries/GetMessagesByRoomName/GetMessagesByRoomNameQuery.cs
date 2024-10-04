using MediatR;

namespace Application.Message.Queries.GetMessagesByRoomName
{
    public class GetMessagesByRoomNameQuery
        : IRequest<IList<Domain.Entity.Message>>

    {
        public string? RoomName { get; set; }
    }
}
