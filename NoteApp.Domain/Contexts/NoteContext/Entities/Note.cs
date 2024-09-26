﻿using NoteApp.Domain.Contexts.AccountContext.Entities;
using NoteApp.Domain.SharedContext.Entities;

namespace NoteApp.Domain.Contexts.NoteContext.Entities;

public class Note : Entity
{
    protected Note() 
    {

    }
    public Note(string title, string body, Guid userId)
    {
        Title = title;
        Body = body;
        UserId = userId;
    }

    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
