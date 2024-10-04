using Domain.Entity;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ChatDbContext _context;

        public ScheduleRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<Schedule> CreateSchedule(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            return schedule;
        }

        public async Task DeleteScheduleById(string? id)
        {
            var schedule = await _context.Schedules
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id.ToString() == id) ?? throw new KeyNotFoundException();

             _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Schedule>> GetSchedules()
            => await _context.Schedules
                    .AsNoTracking()
                    .ToListAsync();

        public async Task<IList<Schedule>> GetSchedulesToExecute()
            => await _context.Schedules
                    .Where(x => x.IsActive && 
                        x.LastExecutedDate.AddSeconds(x.SecondsToExecute.HasValue ? (double)x.SecondsToExecute : 0) <= DateTime.Now)
                    .AsNoTracking()
                    .ToListAsync();

        public async Task<Schedule> UpdateScheduleById(string? id, Schedule schedule)
        {
            var scheduleInDatabase = await _context.Schedules
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.ToString() == id) ?? throw new KeyNotFoundException();

            scheduleInDatabase.Name = schedule.Name;
            scheduleInDatabase.LastExecutedDate = schedule.LastExecutedDate;
            scheduleInDatabase.SecondsToExecute = schedule.SecondsToExecute;
            scheduleInDatabase.Type = schedule.Type;
            scheduleInDatabase.RoomNames = schedule.RoomNames;
            scheduleInDatabase.IsActive = schedule.IsActive;

            _context.Schedules.Update(scheduleInDatabase);

            await _context.SaveChangesAsync();

            return scheduleInDatabase; 
        }
    }
}
