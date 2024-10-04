using Domain.Interface;
using MediatR;

namespace Application.Schedule.Queries.GetSchedulesToExecute
{
    public class GetSchedulesToExecuteQueryHandler
        : IRequestHandler<GetSchedulesToExecuteQuery, IList<Domain.Entity.Schedule>>

    {
        private readonly IScheduleRepository _scheduleRepository;

        public GetSchedulesToExecuteQueryHandler(
            IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository
                ?? throw new ArgumentNullException(nameof(scheduleRepository));
        }
        public async Task<IList<Domain.Entity.Schedule>> Handle(GetSchedulesToExecuteQuery request, CancellationToken cancellationToken)
            => await _scheduleRepository.GetSchedulesToExecute();

    }
}
