using Flunt.Notifications;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.SendVerificationCode;

public class Response : SharedContext.UseCases.Response
{
    public Response(
        string message, 
        int status, 
        IEnumerable<Notification>? notifications = null) : base(message, status, notifications)
    {
    }
}
