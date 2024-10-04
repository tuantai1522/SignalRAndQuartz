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
        {
            var schedules = await _context.Schedules
                .AsNoTracking()
                .ToListAsync();

            IList<Schedule> result = [];

            foreach (var x in schedules)
            {
                if (x.IsActive && x.NextExecutedDate.AddSeconds(x.SecondsToExecute) <= DateTime.Now)
                    result.Add(x);
            }

            return result;
        }


        public async Task UpdateActiveByScheduleId(string? id, bool status = false)
        {
            var scheduleInDatabase = await _context.Schedules
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.ToString() == id) ?? throw new KeyNotFoundException();

            scheduleInDatabase.IsActive = status;
            scheduleInDatabase.NextExecutedDate = DateTime.Now.AddHours(7);

            _context.Schedules.Update(scheduleInDatabase);

            await _context.SaveChangesAsync();
        }

        public async Task<Schedule> UpdateScheduleById(string? id, Schedule schedule)
        {
            var scheduleInDatabase = await _context.Schedules
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.ToString() == id) ?? throw new KeyNotFoundException();

            scheduleInDatabase.Name = schedule.Name;
            scheduleInDatabase.NextExecutedDate = schedule.NextExecutedDate;
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
