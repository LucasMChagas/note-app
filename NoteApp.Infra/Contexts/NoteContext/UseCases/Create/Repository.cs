using Microsoft.EntityFrameworkCore;
using NoteApp.Domain.Contexts.NoteContext.Entities;
using NoteApp.Domain.Contexts.NoteContext.UseCases.Create.Contracts;
using NoteApp.Infra.Data;

namespace NoteApp.Infra.Contexts.NoteContext.UseCases.Create;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AnyAsync(Guid Id, CancellationToken cancellationToken)
    {
        return await _context
            .Users
            .AsNoTracking()
            .AnyAsync(x => x.Id == Id, cancellationToken);            
    }

    public async Task SaveAsync(Note note, CancellationToken cancellationToken)
    {
        await _context.Notes.AddAsync(note, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
