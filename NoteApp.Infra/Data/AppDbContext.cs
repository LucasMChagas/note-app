using Microsoft.EntityFrameworkCore;
using NoteApp.Domain.Contexts.AccountContext.Entities;
using NoteApp.Domain.Contexts.NoteContext.Entities;
using NoteApp.Infra.Contexts.AccountContext.Mappings;
using NoteApp.Infra.Contexts.NoteContext.Mappings;

namespace NoteApp.Infra.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Note> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new NoteMap());
    }
}
