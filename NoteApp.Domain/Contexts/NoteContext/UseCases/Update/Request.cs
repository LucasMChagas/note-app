using MediatR;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.Update;

public class Request : IRequest<Response>
{
    public string UserId { get; set; } = string.Empty;
    public string NoteId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
