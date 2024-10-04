using MediatR;

namespace Application.Schedule.Queries.GetSchedulesToExecute
{
    public class GetSchedulesToExecuteQuery
        : IRequest<IList<Domain.Entity.Schedule>>
    {
    }
}
