using Microsoft.Extensions.DependencyInjection;
using NoteApp.Domain.AccountContext.ValueObjects;
using NoteApp.Domain.Contexts.AccountContext.Entities;
using NoteApp.Domain.Contexts.AccountContext.ValueObjects;
using NoteApp.Infra.Data;

namespace NoteApp.Tests.Data;

public class NoteAppMockData
{
    public static async Task CreateUsers(NoteAppApiApplication application, bool criar)
    {
        using (var scope = application.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            using (var noteAppDbContext = provider.GetRequiredService<AppDbContext>())
            {
                await noteAppDbContext.Database.EnsureCreatedAsync();

                if (criar)
                {
                    var email1 = new Email("lucas@gmail.com");
                    var password1 = new Password("123456789@");
                    var user1 = new User("Lucas", email1, password1);
                    await noteAppDbContext.Users.AddAsync(user1);

                    var email2 = new Email("lucasadm@gmail.com");
                    var password2 = new Password("123456789@");
                    var user2 = new User("Lucas", email2, password2);
                    var code = user2.Email.Verification.Code;
                    user2.Email.Verification.Verify(code);
                    await noteAppDbContext.Users.AddAsync(user2);

                    await noteAppDbContext.SaveChangesAsync();
                }
            }
        }
    }
}
