using MediatR;

namespace Application.Sender.Queries.GetSenderByUserName
{
    public class GetSenderByUserNameQuery
        : IRequest<Domain.Entity.Sender>

    {
        public string? UserName { get; set; }
    }
}
