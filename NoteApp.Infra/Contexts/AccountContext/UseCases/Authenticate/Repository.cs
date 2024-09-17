using Microsoft.EntityFrameworkCore;
using NoteApp.Domain.Contexts.AccountContext.Entities;
using NoteApp.Domain.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using NoteApp.Infra.Data;

namespace NoteApp.Infra.Contexts.AccountContext.UseCases.Authenticate;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByEmail(string email, CancellationToken cancellationToken)
    {
        var user = await _context
           .Users
           .AsNoTracking()
           .Include(x => x.Roles)
           .FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken);        

        return user;
    }
}
