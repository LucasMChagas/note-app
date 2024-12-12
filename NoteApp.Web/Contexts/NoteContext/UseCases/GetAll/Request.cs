namespace NoteApp.Web.Contexts.NoteContext.UseCases.GetAll;

public class Request(string Token) : Domain.Contexts.NoteContext.UseCases.GetAll.Request
{
    public string Token { get; set; } = Token;
}