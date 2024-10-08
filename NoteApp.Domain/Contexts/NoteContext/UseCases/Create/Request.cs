﻿using MediatR;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.Create;

public class Request : IRequest<Response>
{
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;

    public Guid UserId { get; set; } 
}
