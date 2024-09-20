using MediatR;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.AccountVerification;

public record Request(string Code, string Email) : IRequest<Response>;


