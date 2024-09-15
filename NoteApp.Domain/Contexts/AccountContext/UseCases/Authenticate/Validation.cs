using Flunt.Notifications;
using Flunt.Validations;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.Authenticate;

public static class Validation
{
    public static Contract<Notification> Ensure(Request request)
    => new Contract<Notification>()
           .Requires()
           .IsLowerThan(request.Password.Length, 40, "Password", "A senha deve conter menos que 40 carcteres")
           .IsGreaterThan(request.Password.Length, 8, "Password", "A senha deve conter no minimo 8 caracteres")
           .IsEmail(request.Email, "Email", "E-mail inválido");
}
