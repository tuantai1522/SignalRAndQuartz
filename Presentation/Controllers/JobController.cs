using Application.ActionType.Queries.GetActionTypes;
using Application.Schedule.Commands.CreateSchedule;
using Application.Schedule.Commands.DeleteScheduleById;
using Application.Schedule.Commands.UpdateActiveByScheduleId;
using Application.Schedule.Commands.UpdateScheduleById;
using Application.Schedule.Queries.GetSchedules;
using Application.Sender.Queries.GetSenderByUserName;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quartz;

namespace Presentation.Controllers
{
    public class JobController : Controller
    {
        private readonly IMediator _mediator;
        public JobController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {
            var schedules = await _mediator.Send(new GetSchedulesQuery());

            return View(schedules);
        }

        public async Task<IActionResult> Create()
        {
            var types = await _mediator.Send(new GetActionTypesQuery());

            ViewBag.Types = types.Select(type => new SelectListItem
            {
                Value = type.TypeName.ToString(),
                Text = type.TypeName.ToString()
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
                string Name, 
                DateTime LastExecutedDate,
                string repeatOption,
                int Year,
                int Month,
                int Day,
                int Hour,
                int Minute,
                int Second,
                string sendToOption,
                string roomName,
                string selectedType)
        {
            long totalSeconds = 0;
            if (repeatOption != "None")
            {
                // Repeat after n seconds => work with year, month, day, ... value
                DateTime currentDate = DateTime.Now;

                DateTime targetDate = currentDate
                    .AddYears(Year)
                    .AddMonths(Month)
                    .AddDays(Day)
                    .AddHours(Hour)
                    .AddMinutes(Minute)
                    .AddSeconds(Second);

                TimeSpan timeSpan = targetDate - currentDate;

                totalSeconds = (long)timeSpan.TotalSeconds;
            }

            selectedType = selectedType ?? "Event";

            await _mediator.Send(new CreateScheduleCommand()
            {
                IsActive = true,
                LastExecutedDate = LastExecutedDate,
                SecondsToExecute = totalSeconds,
                Name = Name,
                Type = selectedType,
                RoomNames = roomName is not null ? roomName.Split(",") : []
            });
            return RedirectToAction("Index", "Job");
        }

        public async Task<IActionResult> DeleteJob(string Id)
        {
            await _mediator.Send(new DeleteScheduleByIdCommand()
            {
                Id = Id,
            });
            return RedirectToAction("Index", "Job");
        }

        public async Task<IActionResult> KillJob(string Id)
        {
            await _mediator.Send(new UpdateActiveByScheduleIdCommand()
            {
                Id = Id,
            });
            return RedirectToAction("Index", "Job");
        }
        public async Task<IActionResult> OpenJob(string Id)
        {
            await _mediator.Send(new UpdateActiveByScheduleIdCommand()
            {
                Id = Id,
                Status = true,
            });
            return RedirectToAction("Index", "Job");
        }
    }
}
