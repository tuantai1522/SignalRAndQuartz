using Domain.Entity;
using Domain.Interface;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ActionTypeRepository : IActionTypeRepository
    {
        private readonly ChatDbContext _context;

        public ActionTypeRepository(ChatDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ActionType>> GetActionTypes()
            => await _context.ActionTypes
                    .AsNoTracking()
                    .ToListAsync();
    }
}
