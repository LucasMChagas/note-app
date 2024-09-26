using NoteApp.Domain.Contexts.NoteContext.Entities;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.Create.Contracts;

public interface IRepository
{
    Task<bool> AnyAsync(Guid Id, CancellationToken cancellationToken);
    Task SaveAsync(Note note, CancellationToken cancellationToken);
}
