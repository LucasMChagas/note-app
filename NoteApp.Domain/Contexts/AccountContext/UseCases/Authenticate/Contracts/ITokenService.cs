using NoteApp.Domain.Contexts.AccountContext.Entities;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.Authenticate.Contracts;

public interface ITokenService
{
    string Create(User user);
}
