using Flunt.Notifications;
using Flunt.Validations;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.Create;

public static class Validation
{
    public static Contract<Notification> Ensure(Request request)
    => new Contract<Notification>()
           .Requires()
           .IsLowerThan(request.Title.Length, 20, "Título", "O Título da nota deve conter menos do que 25 caracteres.")
           .IsNotEmpty(request.Title, "Título","O Título da nota deve set fornecido!")
           .IsLowerThan(request.Body.Length, 500, "Code", "O tamanho da nota não deve ultrapassar 500 caracteres.")
           .IsNotEmpty(request.Body, "Corpo","O conteúdo da nota não foi fornecido")
           .IsNotEmpty(request.UserId, "Id do Usuário", "O id do usuário deve ser fornecido.");
           
}
