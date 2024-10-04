using MediatR;

namespace Application.Schedule.Commands.CreateSchedule
{
    public class CreateScheduleCommand
        : IRequest<bool>
    {
        public string? Name { get; set; }
        public DateTime LastExecutedDate { get; set; }
        public long SecondsToExecute { get; set; }
        public string? Type { get; set; }
        public IList<string> RoomNames { get; set; } = [];
        public bool IsActive { get; set; }
    }
}
