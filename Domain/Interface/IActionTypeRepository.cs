using Domain.Entity;

namespace Domain.Interface
{
    public interface IActionTypeRepository
    {
        Task<IEnumerable<ActionType>> GetActionTypes();

    }
}
