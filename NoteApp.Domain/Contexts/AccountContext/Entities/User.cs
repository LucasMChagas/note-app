using NoteApp.Domain.AccountContext.ValueObjects;
using NoteApp.Domain.Contexts.AccountContext.ValueObjects;
using NoteApp.Domain.Contexts.NoteContext.Entities;
using NoteApp.Domain.SharedContext.Entities;

namespace NoteApp.Domain.Contexts.AccountContext.Entities;

public class User : Entity
{
    protected User()
    {

    }
    public User(string name, Email email, Password password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
    public string Name { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
    public string Image { get; private set; } = string.Empty;
    public List<Note> Notes { get; set; } = [];

    public void UpdatePassword(string plainTextPassword, string code)
    {
        if (!string.Equals(code.Trim(), Password.ResetCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new Exception("Código de restauração inválido");

        var password = new Password(plainTextPassword);
        Password = password;
    }

    public void UpdateEmail(Email email)
    {
        Email = email;
    }

    public void ChangePassword(string plainTextPassword)
    {
        var password = new Password(plainTextPassword);
        Password = password;
    }
}
