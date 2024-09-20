using MediatR;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.SendVerificationCode;

public record Request(string Email) : IRequest<Response>;

