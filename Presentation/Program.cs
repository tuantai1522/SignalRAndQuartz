using Application;
using Domain.Interface;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Application.Message.Commands.CreateMessage;
using Application.Message.Queries.GetListMessage;
using Application.Sender.Commands.CreateSender;
using Application.Sender.Queries.GetListSender;
using Application.Sender.Queries.GetSenderByUserName;
using Application.Room.Commands.CreateRoom;
using Application.Room.Queries.GetListRoom;
using Application.Room.Queries.GetRoomByRoomName;
using Infrastructure.Services;
using Application.Message.Queries.GetMessageById;
using Quartz;
using Application.Schedule.Queries.GetSchedules;
using Application.Jobs;
using Application.Schedule.Commands.CreateSchedule;
using Application.Schedule.Commands.DeleteScheduleById;
using Application.Schedule.Commands.UpdateScheduleById;
using Application.ActionType.Queries.GetActionTypes;
using Application.Schedule.Queries.GetSchedulesToExecute;
using Application.Schedule.Commands.UpdateActiveByScheduleId;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddSignalR();

        builder.Services.AddScoped<IMessageRepository, MessageRepository>();
        builder.Services.AddScoped<ISenderRepository, SenderRepository>();
        builder.Services.AddScoped<IRoomRepository, RoomRepository>();
        builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
        builder.Services.AddScoped<IActionTypeRepository, ActionTypeRepository>();

        builder.Services.AddScoped<IStreamService, StreamService>();

        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<CreateMessageCommand>());
        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<GetListMessageQuery>());
        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<GetMessageByIdQuery>());

        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<CreateSenderCommand>());
        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<GetListSenderQuery>());
        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<GetSenderByUserNameQuery>());

        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<CreateRoomCommand>());
        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<GetListRoomQuery>());
        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<GetRoomByRoomNameQuery>());

        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<GetSchedulesQuery>());
        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<GetSchedulesToExecuteQuery>());
        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<CreateScheduleCommand>());
        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<DeleteScheduleByIdCommand>());
        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<UpdateScheduleByIdCommand>());
        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<UpdateActiveByScheduleIdCommand>());

        builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<GetActionTypesQuery>());


        builder.Services.AddDbContext<ChatDbContext>(x => x
            .EnableSensitiveDataLogging()
            .UseMongoDB(builder.Configuration.GetConnectionString("MongoDb")!, "chat")
        );

        
        builder.Services.AddQuartz(q =>
        {
            var jobKey = new JobKey("notifyScheduleJob", "scheduleGroup");
            q.AddJob<ExecuteJobInDatabase>(opts => opts
                    .WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("notifyScheduleJob_trigger", "scheduleGroup_trigger")
                .WithCronSchedule("0/5 * * * * ?"));
        });

        builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            await app.InitializeDatabaseAsync();

            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.MapHub<Chat>("/chat");

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}