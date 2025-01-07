using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NoteApp.Domain.Contexts.NoteContext.Entities;
using NoteApp.Web.Contexts.NoteContext.UseCases.Update;

namespace NoteApp.Web.Components.Contexts.NotesContext;

public partial class NotesEditor : ComponentBase
{
    [Inject] 
    public IRequestHandler<
        NoteApp.Web.Contexts.NoteContext.UseCases.Update.Request,
        NoteApp.Domain.Contexts.NoteContext.UseCases.Update.Response> HandleUpdateRequest { get; set; } = null!;

    [Inject] 
    public IJSRuntime JSRuntime { get; set; } = null!;
    
    [Parameter]
    public Note Note { get; set;} 
    
    [Parameter] 
    public EventCallback<NoteUpdate> UpdateNoteList { get; set; }
    
    [CascadingParameter]
    public string Token { get; set; }
    
    private async Task OnContentChanged()
    {
        var request = new Request();
        request.Token = Token;
        request.NoteId = Note.Id;
        request.Title = Note.Title;
        request.Body = Note.Body;
        
        await HandleUpdateRequest.Handle(request, new CancellationToken());
    }
}