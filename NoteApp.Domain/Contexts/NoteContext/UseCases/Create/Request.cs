using MediatR;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.Create;

public class Request : IRequest<Response>
{
    public string Title { get; set; }
    public string Body { get; set; }

    public Guid UserId { get; set; }
}
