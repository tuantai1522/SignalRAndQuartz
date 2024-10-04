using Application.Room.Queries.GetListRoom;
using Domain.Interface;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Schedule.Queries.GetSchedules
{
    public class GetSchedulesQueryHandler
        : IRequestHandler<GetSchedulesQuery, IList<Domain.Entity.Schedule>>

    {
        private readonly IScheduleRepository _scheduleRepository;

        public GetSchedulesQueryHandler(
            IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository
                ?? throw new ArgumentNullException(nameof(scheduleRepository));
        }
        public async Task<IList<Domain.Entity.Schedule>> Handle(
                GetSchedulesQuery request, 
                CancellationToken cancellationToken)
            => await _scheduleRepository.GetSchedules();

    }
}
