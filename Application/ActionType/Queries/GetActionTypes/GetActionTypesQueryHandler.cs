using Application.Schedule.Commands.CreateSchedule;
using Domain.Entity;
using Domain.Interface;
using MediatR;

namespace Application.ActionType.Queries.GetActionTypes
{
    public class GetActionTypesQueryHandler
        : IRequestHandler<GetActionTypesQuery, IEnumerable<Domain.Entity.ActionType>>

    {
        private readonly IActionTypeRepository _actionTypeRepository;

        public GetActionTypesQueryHandler(
            IActionTypeRepository actionTypeRepository)
        {
            _actionTypeRepository = actionTypeRepository
                ?? throw new ArgumentNullException(nameof(actionTypeRepository));
        }

        public async Task<IEnumerable<Domain.Entity.ActionType>> Handle(GetActionTypesQuery request, CancellationToken cancellationToken)
            => await _actionTypeRepository.GetActionTypes();
    }
}
