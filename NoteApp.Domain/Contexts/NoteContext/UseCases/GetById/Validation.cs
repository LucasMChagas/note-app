using Flunt.Notifications;
using Flunt.Validations;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.GetById;

public static class Validation
{
    public static Contract<Notification> Ensure(Request request)
    => new Contract<Notification>()
           .Requires()
           .IsLowerThan(request.NoteId, 37, "Id Nota", "Formato de Id da nota errado!")
           .IsGreaterThan(request.NoteId, 35, "Id Nota", "Formato de Id da nota errado!")
           .IsLowerThan(request.UserId, 37, "Id da conta", "Formato de Id da conta errado!")
           .IsGreaterThan(request.UserId, 35, "Id da conta", "Formato de Id da conta errado!");
}
