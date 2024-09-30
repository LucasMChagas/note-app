using NoteApp.Domain.Contexts.NoteContext.Entities;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.GetById.Contracts;

public interface IRepository
{
    Task<bool> UserIsAuthorAsync(Guid noteId, Guid userId, CancellationToken cancellationToken);
    Task<Note> GetByIdAsync(Guid noteId, CancellationToken cancellationToken);
}
