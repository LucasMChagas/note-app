using Flunt.Notifications;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.Authenticate;

public class Response : SharedContext.UseCases.Response
{
    protected Response()
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
        IEnumerable<Notification>? notifications = null) : base(message, 200, notifications) 
    {        
        Data = data;
    }

    public ResponseData? Data { get; set; }
}
public class ResponseData
{
    public string Token { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string[] Roles { get; set; } = Array.Empty<string>();
}
