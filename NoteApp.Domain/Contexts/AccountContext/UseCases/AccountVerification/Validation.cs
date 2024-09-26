using Flunt.Notifications;
using Flunt.Validations;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.AccountVerification;

public static class Validation
{
    public static Contract<Notification> Ensure(Request request)
    => new Contract<Notification>()
           .Requires()
           .IsLowerThan(request.Code.Length, 7, "Code", "Cógigo inválido")
           .IsGreaterThan(request.Code.Length, 5, "Code", "Código inválido")
           .IsEmail(request.Email, "Email", "E-mail inválido");
}
