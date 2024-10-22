using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using NoteApp.Domain.Services;
using NoteApp.Infra.Data;
using NoteApp.Tests.Services;

namespace NoteApp.Tests.Integration;

public class NoteAppApiApplication : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var root = new InMemoryDatabaseRoot();

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
            services.RemoveAll(typeof(ISendEmailService));

            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("NoteAppDatabase", root));

            services.AddSingleton<ISendEmailService, MockSendEmailService>();
        });

        return base.CreateHost(builder);
    }
}
