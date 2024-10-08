﻿using MediatR;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.Authenticate;

public record Request(
    string Email, 
    string Password
) : IRequest<Response>;

