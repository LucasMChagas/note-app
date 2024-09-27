using MediatR;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.Delete;

public class Request : IRequest<Response> 
{    
    public Guid NoteId { get; set; }
    public Guid UserId { get; set; }
};

