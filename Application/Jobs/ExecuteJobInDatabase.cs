using Application.Room.Queries.GetListRoom;
using Application.Schedule.Commands.UpdateScheduleById;
using Application.Schedule.Queries.GetSchedulesToExecute;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Quartz;

namespace Application.Jobs
{
    public class ExecuteJobInDatabase : IJob
    {
        private readonly ChatDbContext _context;
        private readonly IMediator _mediator;
        private readonly IHubContext<Chat> _chat;

        public ExecuteJobInDatabase(ChatDbContext context, IHubContext<Chat> chat,
                IMediator mediator)
        {
            _context = context;
            _chat = chat;
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
           var schedules = await _mediator.Send(new GetSchedulesToExecuteQuery());            

            foreach (var schedule in schedules)
            {
                //Send to specific room
                if (schedule.RoomNames.Count != 0)
                {
                    foreach (string roomName in schedule.RoomNames)
                    {
                        await _chat.Clients.Group(roomName)
                            .SendAsync("ReceiveEvent", new
                            {
                                Content = schedule.Name,
                            });
                    }
                }
                else
                {
                    var rooms = await _mediator.Send(new GetListRoomQuery());

                    foreach(var room in rooms)
                    {
                        //Send to all
                        await _chat.Clients.Group(room.RoomName)
                            .SendAsync("ReceiveEvent", new
                            {
                                Content = schedule.Name,
                            });
                    }


                }

                if (schedule.SecondsToExecute != 0)
                {
                    // execute multiple times
                    schedule.LastExecutedDate = DateTime.Now.AddSeconds((double)schedule.SecondsToExecute);
                }
                else
                {
                    //execute only once and stop it
                    schedule.IsActive = false;
                }

                //Update again in database
                await _mediator.Send(new UpdateScheduleByIdCommand()
                {
                    Id = schedule.Id.ToString(),
                    Schedule = schedule,
                }) ;
            }


        }
    }
}
