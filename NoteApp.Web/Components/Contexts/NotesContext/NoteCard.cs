using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using NoteApp.Domain.Contexts.NoteContext.Entities;
using NoteApp.Web.Components.SharedContext;
using NoteApp.Web.Contexts.NoteContext.UseCases.Delete;

namespace NoteApp.Web.Components.Contexts.NotesContext;

public partial class NoteCard : ComponentBase
{
    [Inject]
    public IDialogService DialogService { get; set; } = null!;
    [Inject]
    public IRequestHandler<
        Web.Contexts.NoteContext.UseCases.Delete.Request, 
        Domain.Contexts.NoteContext.UseCases.Delete.Response> HandlerRequest { get; set; } = null!;
    
    [Parameter] 
    public List<Note> Notes { get; set; } = [];
    [Parameter] 
    public EventCallback<Note> OnDataSend { get; set; }
    [Parameter] 
    public EventCallback<int> SetAmountNotes { get; set; }
    [CascadingParameter]
    public string Token { get; set; } 
    
    private List<bool> _activeDivs = [];
    private void ToggleDiv(int index)
    {
        for (int i = 0; i < _activeDivs.Count; i++)
        {
            _activeDivs[i] = false;
        } 
        _activeDivs[index] = true;
    }

    private string GetClass(int index)
    {
        return _activeDivs[index] ? "note-card is-active" : "note-card";
    }
    
    protected override Task OnParametersSetAsync()
    {
        if (Notes != null && ( _activeDivs == null || _activeDivs.Count != Notes.Count))
        {
            _activeDivs = Enumerable.Repeat(false, Notes.Count).ToList();
            Console.WriteLine($"_activeDivs inicializado em OnParametersSetAsync com {Notes.Count} itens.");
        }
        return Task.CompletedTask;
    }
    private async void SendData(Note note)
    {
        await OnDataSend.InvokeAsync(note);
    }

    private async Task DeleteNote(Guid id)
    {
        var parameters = new DialogParameters<ConfirmDialog>
        {
            { x => x.ContentText, "Você tem certeza que deseja excluir essa nota?" },
            { x => x.ButtonText, "Sim" },
            { x => x.Color, Color.Success }
        }; 
        
        var dialogResult = await DialogService.Show<ConfirmDialog>("Confirm", parameters).Result;
        
        if (!dialogResult.Canceled) 
        { 
            var request = new Request
            {
                NoteId = id,
                Token = this.Token
            };
            HandlerRequest.Handle(request, new CancellationToken());
            var note = Notes.FirstOrDefault(x => x.Id == id);
            Notes.Remove(note);
            SetAmountNotes.InvokeAsync(Notes.Count);
        }
    }
}