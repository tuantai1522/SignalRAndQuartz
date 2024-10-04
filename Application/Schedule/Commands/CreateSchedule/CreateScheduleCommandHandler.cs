using Application.Room.Commands.CreateRoom;
using Domain.Interface;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Schedule.Commands.CreateSchedule
{
    public class CreateScheduleCommandHandler
        : IRequestHandler<CreateScheduleCommand, bool>

    {
        private readonly IScheduleRepository _scheduleRepository;

        public CreateScheduleCommandHandler(
            IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository
                ?? throw new ArgumentNullException(nameof(scheduleRepository));
        }
        public async Task<bool> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = new Domain.Entity.Schedule()
            {
                Name = request.Name,
                IsActive = request.IsActive,
                LastExecutedDate = request.LastExecutedDate.AddHours(7),
                SecondsToExecute = request.SecondsToExecute,
                RoomNames = request.RoomNames,
                Type = request.Type,
            };

            await _scheduleRepository.CreateSchedule(schedule);

            return true;
        }
    }
}
