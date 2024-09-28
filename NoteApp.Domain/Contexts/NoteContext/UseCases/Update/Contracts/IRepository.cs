namespace NoteApp.Domain.Contexts.NoteContext.UseCases.Update.Contracts;

public interface IRepository
{
    Task<bool> UserIsAuthorAsync(Guid userId, Guid noteId, CancellationToken cancellationToken);
    Task UpdateAsync(Guid noteId, string title, string body, CancellationToken cancellationToken);
}
