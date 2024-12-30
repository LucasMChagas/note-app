namespace NoteApp.Web.Contexts.NoteContext.UseCases.Update;

public class Request : Domain.Contexts.NoteContext.UseCases.Update.Request
{
    public string Token { get; set; }
    public Guid NoteId { get; set; }
}