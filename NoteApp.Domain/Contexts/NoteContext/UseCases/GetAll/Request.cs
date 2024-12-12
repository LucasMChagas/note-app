using MediatR;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.GetAll;

public class Request : IRequest<Response>
{
    public Request()
    {
    }

    public string UserId { get; set; } = string.Empty;    
    public int PageNumber { get; set; } = Configuration.Pagination.DefaultPageNumber;
    public int PageSize { get; set; } = Configuration.Pagination.DefaultPageSize;
}
