using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Schedule.Commands.DeleteScheduleById
{
    public class DeleteScheduleByIdCommand
        : IRequest<bool>

    {
        public string? Id { get; set; }
    }
}
