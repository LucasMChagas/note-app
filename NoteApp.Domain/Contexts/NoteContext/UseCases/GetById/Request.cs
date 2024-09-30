using MediatR;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.GetById;

public class Request : IRequest<Response>
{
    public string NoteId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}
