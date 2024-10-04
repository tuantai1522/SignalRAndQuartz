using Domain.Entity;

namespace Domain.Interface
{
    public interface IScheduleRepository
    {
        Task<IList<Schedule>> GetSchedules();
        Task<IList<Schedule>> GetSchedulesToExecute();
        Task<Schedule> CreateSchedule(Schedule schedule);
        Task DeleteScheduleById(string? id);
        Task<Schedule> UpdateScheduleById(string? id, Schedule schedule);
        Task UpdateActiveByScheduleId(string? id, bool status = false);

    }
}
