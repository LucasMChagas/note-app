using NoteApp.Domain.Contexts.AccountContext.Entities;

namespace NoteApp.Domain.Services;

public interface ISendEmailService
{
    Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken);
}
