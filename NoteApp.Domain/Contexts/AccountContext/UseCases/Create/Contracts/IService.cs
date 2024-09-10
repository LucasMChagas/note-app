using NoteApp.Domain.Contexts.AccountContext.Entities;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.Create.Contracts;

public interface IService
{
    Task SendVerificatioEmailAsync(User user, CancellationToken cancellationToken);
}
