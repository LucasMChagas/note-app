﻿using NoteApp.Domain.SharedContext.ValueObjects;

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
            throw new Exception("Este item já foi ativado");

        if (ExpiresAt < DateTime.UtcNow)
            throw new Exception("Este código já expirou");

        if (!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new Exception("Código inválido!");

        ExpiresAt = null;
        VerifiedAt = DateTime.UtcNow;
    }

    public void NewCode()
    {
        Code = Guid.NewGuid().ToString("N")[..6].ToUpper();
        ExpiresAt = DateTime.UtcNow.AddMinutes(15);
    }
}
