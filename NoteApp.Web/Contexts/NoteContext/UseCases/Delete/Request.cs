namespace NoteApp.Web.Contexts.NoteContext.UseCases.Delete;

public class Request : Domain.Contexts.NoteContext.UseCases.Delete.Request
{
    public string Token { get; set; }
    public Guid NoteId { get; set; }
}