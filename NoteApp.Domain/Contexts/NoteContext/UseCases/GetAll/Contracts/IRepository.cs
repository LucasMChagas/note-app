using NoteApp.Domain.Contexts.NoteContext.Entities;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.GetAll.Contracts;

public interface IRepository
{
    Task<List<Note>> GetNotesAsync(Guid userId, int page, int perPage, CancellationToken cancellationToken);
}
