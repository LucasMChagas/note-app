using Microsoft.EntityFrameworkCore;
using NoteApp.Domain.Contexts.AccountContext.Entities;
using NoteApp.Domain.Contexts.AccountContext.UseCases.AccountVerification.Contracts;
using NoteApp.Infra.Data;

namespace NoteApp.Infra.Contexts.AccountContext.UseCases.AccountVerification;

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

    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
