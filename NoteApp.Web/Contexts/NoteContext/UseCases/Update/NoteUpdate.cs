namespace NoteApp.Web.Contexts.NoteContext.UseCases.Update;

public class NoteUpdate
{
    public NoteUpdate(Guid id, string title, string body)
    {
        Id = id;
        Title = title;
        Body = body;
    }

    public Guid? Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}