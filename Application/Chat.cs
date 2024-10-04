using Application.Room.Commands.CreateRoom;
using Application.Room.Queries.GetRoomByRoomName;
using Domain.Entity;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Core.Connections;
using System.Collections.Concurrent;

namespace Application
{

    public class Chat : Hub
    {
        public Task JoinRoom(string roomName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

    }

}
