using NoteApp.Domain.Contexts.AccountContext.Entities;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.SendVerificationCode.Contracts;

public interface IRepository
{
    Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
}
