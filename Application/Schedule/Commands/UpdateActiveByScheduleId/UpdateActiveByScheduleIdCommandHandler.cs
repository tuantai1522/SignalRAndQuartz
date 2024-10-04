using Domain.Interface;
using MediatR;

namespace Application.Schedule.Commands.UpdateActiveByScheduleId
{
    public class UpdateActiveByScheduleIdCommandHandler
        : IRequestHandler<UpdateActiveByScheduleIdCommand, bool>

    {
        private readonly IScheduleRepository _scheduleRepository;

        public UpdateActiveByScheduleIdCommandHandler(
            IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository
                ?? throw new ArgumentNullException(nameof(scheduleRepository));
        }

        public async Task<bool> Handle(UpdateActiveByScheduleIdCommand request, CancellationToken cancellationToken)
        {
            await _scheduleRepository.UpdateActiveByScheduleId(request.Id, request.Status);

            return true;
        }
    }
}
