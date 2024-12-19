namespace NoteApp.Web.Contexts.NoteContext.UseCases.Create;

public class Request : Domain.Contexts.NoteContext.UseCases.Create.Request
{
    public string Token { get; set; }
}