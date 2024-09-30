using Microsoft.EntityFrameworkCore;
using NoteApp.Domain.Contexts.NoteContext.UseCases.Delete.Contracts;
using NoteApp.Infra.Data;

namespace NoteApp.Infra.Contexts.NoteContext.UseCases.Delete;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task DeleteAsync(Guid noteId, CancellationToken cancellationToken)
    {
         var note = await _context
            .Notes
            .FirstOrDefaultAsync(n => n.Id == noteId, cancellationToken) 
            ?? throw new Exception("Nota não encontrada!");

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UserIsAuthorAsync(Guid userId, Guid noteId, CancellationToken cancellationToken)
    {
        var note = await _context
            .Notes
            .AsNoTracking()
            .FirstOrDefaultAsync(n => n.Id == noteId, cancellationToken);

        if(note is null)
            return false;

        if(note.UserId == userId) 
            return true;

        return false;
    }
}
