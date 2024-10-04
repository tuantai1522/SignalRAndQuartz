using MediatR;

namespace Application.Schedule.Queries.GetSchedules
{
    public class GetSchedulesQuery
        : IRequest<IList<Domain.Entity.Schedule>>
    {
    }
}
