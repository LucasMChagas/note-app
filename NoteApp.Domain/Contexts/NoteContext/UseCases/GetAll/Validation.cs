using Flunt.Notifications;
using Flunt.Validations;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.GetAll;

public static class Validation
{
    public static Contract<Notification> Ensure(Request request)
    => new Contract<Notification>()
           .Requires()
           .IsLowerThan(request.UserId.Length, 37, "Id da conta", "Formato de Id da conta errado!")
           .IsGreaterThan(request.UserId.Length, 35, "Id da conta", "Formato de Id da conta errado!")
           .IsGreaterThan(request.PageNumber, 0, "Numero da página", "Numero da página não pode ser menor do que um")
           .IsLowerThan(request.PageSize, 21, "Quantidade por página", "A quantidade por requisição não pode ser superior a 20")
           .IsGreaterThan(request.PageSize, 9, "Quantidade por página", "A quantidade por requisição não pode ser menor do que 10");
}
