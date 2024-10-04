using Application.Schedule.Commands.DeleteScheduleById;
using Domain.Interface;
using MediatR;

namespace Application.Schedule.Commands.UpdateScheduleById
{
    public class UpdateScheduleByIdCommandHandler
        : IRequestHandler<UpdateScheduleByIdCommand, Domain.Entity.Schedule>

    {
        private readonly IScheduleRepository _scheduleRepository;

        public UpdateScheduleByIdCommandHandler(
            IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository
                ?? throw new ArgumentNullException(nameof(scheduleRepository));
        }
        public async Task<Domain.Entity.Schedule> Handle(UpdateScheduleByIdCommand request, CancellationToken cancellationToken)
        {
            var newSchedule = await _scheduleRepository.UpdateScheduleById(request.Id, request.Schedule);

            return newSchedule;
        }
    }
}
