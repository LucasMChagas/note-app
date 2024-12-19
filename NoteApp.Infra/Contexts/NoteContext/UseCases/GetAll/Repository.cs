using Microsoft.EntityFrameworkCore;
using NoteApp.Domain.Contexts.NoteContext.Entities;
using NoteApp.Domain.Contexts.NoteContext.UseCases.GetAll.Contracts;
using NoteApp.Infra.Data;
using System.Linq;

namespace NoteApp.Infra.Contexts.NoteContext.UseCases.GetAll;

public class Repository : IRepository
{
    private readonly AppDbContext _context;
    
    public Repository(AppDbContext context)
    {
        _context = context; 
    }
    public async Task<List<Note>> GetNotesAsync(Guid userId, int page, int perPage, CancellationToken cancellationToken)
    {
        return await _context
            .Notes
            .AsNoTracking()
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .Skip((page - 1) * perPage)
            .Take(perPage)            
            .ToListAsync(cancellationToken);
    }
}
