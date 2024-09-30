using Microsoft.EntityFrameworkCore;
using NoteApp.Domain.Contexts.NoteContext.Entities;
using NoteApp.Domain.Contexts.NoteContext.UseCases.GetById.Contracts;
using NoteApp.Infra.Data;

namespace NoteApp.Infra.Contexts.NoteContext.UseCases.GetById;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Note> GetByIdAsync(Guid noteId, CancellationToken cancellationToken)
    {
        var note = await _context
            .Notes
            .AsNoTracking()
            .FirstOrDefaultAsync(n => n.Id == noteId, cancellationToken);

        return note;
    }

    public async Task<bool> UserIsAuthorAsync(Guid noteId, Guid userId, CancellationToken cancellationToken)
    {
        var note = await _context
            .Notes
            .AsNoTracking()
            .FirstOrDefaultAsync(n => n.Id == noteId, cancellationToken);

        if (note is null)
            return false;

        if (note.UserId == userId)
            return true;

        return false;
    }
}
