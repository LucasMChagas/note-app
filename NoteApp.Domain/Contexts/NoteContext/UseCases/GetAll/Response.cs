using Flunt.Notifications;
using NoteApp.Domain.Contexts.NoteContext.Entities;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.GetAll;

public class Response : SharedContext.UseCases.Response
{
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

    public Response()
    {
    }
    public ResponseData? Data { get; set; }
}

public record ResponseData(List<Note> Notes);
