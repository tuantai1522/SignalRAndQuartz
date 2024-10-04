using Application;
using Application.Message.Commands.CreateMessage;
using Application.Message.Queries.GetListMessage;
using Application.Sender.Commands.CreateSender;
using Application.Sender.Queries.GetListSender;
using Application.Sender.Queries.GetSenderByUserName;
using BlazorAppView.Client.Pages;
using BlazorAppView.Components;
using Domain.Interface;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.MapHub<Application.Chat>("/chat");
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorAppView.Client._Imports).Assembly);

app.Run();
