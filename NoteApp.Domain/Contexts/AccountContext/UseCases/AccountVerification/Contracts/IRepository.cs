using NoteApp.Domain.Contexts.AccountContext.Entities;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.AccountVerification.Contracts;

public interface IRepository
{
    Task<User> GetUserByEmail(string email, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
}
