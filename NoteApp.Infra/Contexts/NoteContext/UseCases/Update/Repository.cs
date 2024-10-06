using Microsoft.EntityFrameworkCore;
using NoteApp.Domain.Contexts.NoteContext.UseCases.Update.Contracts;
using NoteApp.Infra.Data;

namespace NoteApp.Infra.Contexts.NoteContext.UseCases.Update;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }
    public async Task UpdateAsync(Guid noteId, string title, string body, CancellationToken cancellationToken)
    {
        var note = await _context
            .Notes
            .FirstOrDefaultAsync(n => n.Id == noteId, cancellationToken)
            ?? throw new Exception("Nota não encontrada!");

        note.Update(title, body);      

        _context.Notes.Update(note);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UserIsAuthorAsync(Guid userId, Guid noteId, CancellationToken cancellationToken)
    {
        var note = await _context
            .Notes
            .FirstOrDefaultAsync(n => n.Id == noteId, cancellationToken);

        if (note is null)
            return false;

        if (note.UserId != userId)
            return false;

        return true;
    }
}
