using Flunt.Notifications;
using Flunt.Validations;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.SendVerificationCode;

public static class Validation
{
    public static Contract<Notification> Ensure(Request request)
    => new Contract<Notification>()
           .Requires()           
           .IsEmail(request.Email, "Email", "E-mail inválido");
}
