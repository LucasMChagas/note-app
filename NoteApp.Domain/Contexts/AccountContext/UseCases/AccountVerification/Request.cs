using MediatR;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.AccountVerification;

public class Request : IRequest<Response>
{
    public string Code { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

