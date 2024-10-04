
using Domain.Interface;
using MediatR;
using ZstdSharp.Unsafe;

namespace Application.Schedule.Commands.DeleteScheduleById
{
    public class DeleteScheduleByIdCommandHandler
        : IRequestHandler<DeleteScheduleByIdCommand, bool>

    {
        private readonly IScheduleRepository _scheduleRepository;

        public DeleteScheduleByIdCommandHandler(
            IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository
                ?? throw new ArgumentNullException(nameof(scheduleRepository));
        }
        public async Task<bool> Handle(DeleteScheduleByIdCommand request, CancellationToken cancellationToken)
        {
            await _scheduleRepository.DeleteScheduleById(request.Id);

            return true;
        }
    }
}
