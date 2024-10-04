
using MediatR;

namespace Application.Schedule.Commands.UpdateScheduleById
{
    public class UpdateScheduleByIdCommand
        : IRequest<Domain.Entity.Schedule>

    {
        public string? Id { get; set; }
        public Domain.Entity.Schedule? Schedule { get; set; }


    }
}
