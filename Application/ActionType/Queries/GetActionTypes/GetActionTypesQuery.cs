using MediatR;

namespace Application.ActionType.Queries.GetActionTypes
{
    public class GetActionTypesQuery
        : IRequest<IEnumerable<Domain.Entity.ActionType>>
    {
    }
}
