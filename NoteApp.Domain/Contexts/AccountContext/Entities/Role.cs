﻿using NoteApp.Domain.SharedContext.Entities;

namespace NoteApp.Domain.Contexts.AccountContext.Entities;

public class Role : Entity
{
    public string Name { get; set; } = string.Empty;
    public List<User> Users { get; set; } = new List<User>();
}
