﻿using Flunt.Notifications;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.Update;

public class Response : SharedContext.UseCases.Response
{
    public Response()
    {
        
    }
    public Response(
        string message, 
        int status, 
        IEnumerable<Notification>? notifications = null) : base(message, status, notifications)
    {
    }

    public Response(string message, ResponseData data)
    {
        Data = data;
        Status = 200;
        Notifications = null;
        Message = message;
    }
    public ResponseData? Data { get; set; }
}

public record ResponseData(string Title, string Body, DateTime UpdatedAt);
