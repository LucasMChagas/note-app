using NoteApp.Domain.Contexts.NoteContext.Entities;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.Delete.Contracts;

public interface IRepository
{
    Task<bool> UserIsAuthorAsync(Guid userId, Guid noteId, CancellationToken cancellationToken);
    Task DeleteAsync(Guid noteId, CancellationToken cancellationToken);
}
