using Flunt.Notifications;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.AccountVerification;

public class Response : SharedContext.UseCases.Response
{
    protected Response()
    {

    }

    public Response(
        string message,
        int status,
        IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        Status = status;
        Notifications = notifications;
    }
}
