using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NoteApp.Infra.Data;

namespace NoteApp.Infra.Factory;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

        optionsBuilder.UseSqlServer(connectionString);      

        return new AppDbContext(optionsBuilder.Options);
    }
}
