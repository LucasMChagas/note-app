using Flunt.Notifications;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.Create;

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

    public Response(
        string message, 
        ResponseData data,
        IEnumerable<Notification>? notifications = null) : base(message, 201, notifications)
    {          
        Data = data;
    }

    public ResponseData? Data { get; set; }    
}
public record ResponseData(Guid Id, string Name, string Email);
