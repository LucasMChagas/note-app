using NoteApp.Domain.AccountContext.ValueObjects;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.Create;

public class Request
{
    public Request()
    {
        
    }
    public Request(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
