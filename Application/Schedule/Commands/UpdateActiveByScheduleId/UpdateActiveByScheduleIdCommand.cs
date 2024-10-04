using MediatR;

namespace Application.Schedule.Commands.UpdateActiveByScheduleId
{
    public class UpdateActiveByScheduleIdCommand
        : IRequest<bool>
    {
        public string? Id { get; set; }
        public bool Status { get; set; } = false;
    }
}
