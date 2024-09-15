using NoteApp.Domain.Contexts.AccountContext.Entities;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.Authenticate.Contracts;

public interface IRepository
{
    Task<User> GetUserByEmail(string email, CancellationToken cancellationToken);
}
