using NoteApp.Domain.SharedContext.ValueObjects;
using SecureIdentity.Password;

namespace NoteApp.Domain.Contexts.AccountContext.ValueObjects;

public class Password : ValueObject
{
    protected Password()
    {

    }

    public Password(string? text = null)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            text = PasswordGenerator.Generate();

        Hash = PasswordHasher.Hash(text);
    }
    public string Hash { get; } = string.Empty;
    public string ResetCode { get; } = Guid.NewGuid().ToString("N")[..8].ToUpper();
}
