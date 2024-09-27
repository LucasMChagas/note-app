using Flunt.Notifications;
using Flunt.Validations;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.Delete;

public static class Validation
{
    public static Contract<Notification> Ensure(Request request)
    => new Contract<Notification>()
           .Requires()
           .IsNotEmpty(request.NoteId, "Nota", "O id da nota deve ser fornecido!")
           .IsNotEmpty(request.UserId, "Usuário", "O id do usuário deve ser fornecido!");
           
}
