using Application.Message.Commands.CreateMessage;
using Application.Message.Queries;
using Application.Message.Queries.GetListMessage;
using Application.Sender.Commands.CreateSender;
using Application.Sender.Queries.GetListSender;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Application.Sender.Queries.GetSenderByUserName;
using Application.Room.Queries.GetRoomByRoomName;
using Application.Room.Commands.CreateRoom;
using Application.Message.Queries.GetMessagesByRoomName;
using Infrastructure.Services;
using Application.Message.Queries.GetMessageById;
using MongoDB.Bson;
using Microsoft.AspNetCore.SignalR;
using Application;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IStreamService _streamService;
        public HomeController(IMediator mediator, IStreamService streamService)
        {
            _mediator = mediator;
            _streamService = streamService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckUserName(string userName, string roomName)
        {
            var sender = await _mediator.Send(new GetSenderByUserNameQuery()
            {
                UserName = userName,
            });

            if (sender == null)
            {
                await _mediator.Send(new CreateSenderCommand()
                {
                    SenderName = userName,
                });
            }

            var room = await _mediator.Send(new GetRoomByRoomNameQuery()
            {
                RoomName = roomName,
            });

            if (room == null)
            {
                await _mediator.Send(new CreateRoomCommand()
                {
                    RoomName = roomName,
                });
            }

            return RedirectToAction("ShowInfo", "Home", new { userName, roomName});
        }

        public async Task<IActionResult> ShowInfo(string userName, string roomName)
        {
            ViewBag.UserName = userName;
            ViewBag.RoomName = roomName;

            var messages = await _mediator.Send(new GetMessagesByRoomNameQuery()
            {
                RoomName = roomName,

            });

            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(
                CreateMessageCommand command, IFormFile? file,
                [FromServices] IHubContext<Chat> chat)
        {
            if (file != null && file.Length > 0)
            {
                var permittedExtensions = new[]
                {
                    ".jpg", ".jpeg", ".png", ".pdf",
                    ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx",
                    ".mp4", ".avi", ".mov", ".wmv"
                };

                var permittedContentTypes = new[]
                {
                    "image/jpeg", "image/png", "application/pdf",
                    "application/msword", 
                    "application/vnd.openxmlformats-officedocument.wordprocessingml.document", 
                    "application/vnd.ms-excel",
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                    "application/vnd.ms-powerpoint", 
                    "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                    "video/mp4", 
                    "video/x-msvideo", 
                    "video/quicktime", 
                    "video/x-ms-wmv"
                };

                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                var fileContentType = file.ContentType.ToLowerInvariant();

                if (!permittedExtensions.Contains(fileExtension) || !permittedContentTypes.Contains(fileContentType))
                    return BadRequest("File type is not allowed to create");

                using var memoryStream = _streamService.CreateMemoryStream();

                await file.CopyToAsync(memoryStream);
                command.File = memoryStream.ToArray();
                command.FileName = file.FileName;
            }

            var message = await _mediator.Send(new CreateMessageCommand()
            {
                Message = command.Message,
                RoomName = command.RoomName,
                UserName = command.UserName,

                File = command.File,
                FileName = command.FileName,
            });

            await chat.Clients.Group(command.RoomName)
                .SendAsync("ReceiveMessage", new
                {
                    Text = command.Message,
                    Name = command.UserName,
                    FileName = command.FileName, // Tên file
                    HasFile = file != null && file.Length > 0, 
                    FileUrl = Url.Action("GetFile", "Home", new { messageId = message.Id }) 
                });

            return RedirectToAction("ShowInfo", new { userName = command.UserName, roomName = command.RoomName });
        }

        [HttpGet]
        public async Task<IActionResult> GetFile(string messageId)
        {
            var message = await _mediator.Send(new GetMessageByIdQuery()
            {
                MessageId = messageId,
            });

            var contentType = GetContentType(message.FileName);
            return File(message.FileData, contentType, message.FileName);
        }

        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls" => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".ppt" => "application/vnd.ms-powerpoint",
                ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                _ => "application/octet-stream", 
            };
        }

    }
}
