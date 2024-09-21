using NoteApp.Domain.Contexts.AccountContext.Entities;

namespace NoteApp.Domain.Services;

public interface ISendEmailService
{
    Task SendVerificatioEmailAsync(User user, CancellationToken cancellationToken);
}
