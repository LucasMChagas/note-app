﻿using System.Text.RegularExpressions;
using Flunt.Notifications;
using NoteApp.Domain.Contexts.AccountContext.ValueObjects;
using NoteApp.Domain.SharedContext.Extensions;
using NoteApp.Domain.SharedContext.ValueObjects;

namespace NoteApp.Domain.AccountContext.ValueObjects;

public partial class Email : ValueObject
{
    private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    protected Email()
    {        
    }

    public Email(string address)
    {
        if (string.IsNullOrEmpty(address))
            AddNotification(new Notification("E-mail","E-mail inválido"));

        Address = address.Trim().ToLower();

        if (Address.Length <= 10)
            AddNotification(new Notification("E-mail", "E-mail inválido"));

        if (!EmailRegex().IsMatch(Address))
            AddNotification(new Notification("E-mail", "E-mail inválido"));
    }
    public string Address { get; } = string.Empty;
    public string Hash => Address.ToBase64();
    public Verification Verification { get; private set; } = new();

    public void ResetVerificationCode() => 
        Verification = new Verification();

    public static implicit operator string(Email email)
        => email.ToString();

    public static implicit operator Email(string address)
        => new(address);
    public override string ToString()
        => Address;

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();
}
