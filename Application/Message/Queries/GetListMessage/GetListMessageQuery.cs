using MediatR;

namespace Application.Message.Queries.GetListMessage
{
    public class GetListMessageQuery
        : IRequest<IList<Domain.Entity.Message>>

    {
    }
}
