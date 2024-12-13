namespace NoteApp.Web.Contexts.NoteContext.UseCases.GetAll;

public class Request : Domain.Contexts.NoteContext.UseCases.GetAll.Request
{
   public string Token { get; set; }
}