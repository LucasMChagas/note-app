using Flunt.Notifications;
using NoteApp.Domain.SharedContext.ValueObjects;

namespace NoteApp.Domain.Contexts.AccountContext.ValueObjects;

public class Verification : ValueObject
{
    public Verification()
    {

    }
    public string Code { get; private set; } = Guid.NewGuid().ToString("N")[..6].ToUpper();
    public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(15);
    public DateTime? VerifiedAt { get; private set; } = null;
    public bool IsActive => VerifiedAt != null && ExpiresAt == null;

    public void Verify(string code)
    {
        if (IsActive)
        {
            AddNotification(new Notification("Ativação", "Este item já foi ativado"));
            return;
        }                        

        if (ExpiresAt < DateTime.UtcNow)
        {
            AddNotification(new Notification("Expiração", "Este código já expirou"));
            return;
        }                   

        if (!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase))
        {
            AddNotification(new Notification("Validade", "Código inválido!"));
            return;
        }                   

        ExpiresAt = null;
        VerifiedAt = DateTime.UtcNow;
    }

    public void NewCode()
    {
        Code = Guid.NewGuid().ToString("N")[..6].ToUpper();
        ExpiresAt = DateTime.UtcNow.AddMinutes(15);
    }
    
}
