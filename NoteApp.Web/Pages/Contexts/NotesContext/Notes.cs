using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using NoteApp.Domain.Contexts.NoteContext.Entities;
using NoteApp.Web.Contexts.NoteContext.UseCases.Update;
using NoteApp.Web.Services;

namespace NoteApp.Web.Pages.Contexts.NotesContext;

public partial class Notes : ComponentBase
{
    [Inject]
    public IRequestHandler<
        NoteApp.Web.Contexts.NoteContext.UseCases.GetAll.Request, 
        NoteApp.Domain.Contexts.NoteContext.UseCases.GetAll.Response> HandleGetAllRequest { get; set; } = null!;
    
    [Inject]
    public IRequestHandler<
        NoteApp.Web.Contexts.NoteContext.UseCases.Create.Request,
        NoteApp.Domain.Contexts.NoteContext.UseCases.Create.Response> HandleCreateRequest { get; set; } = null!;
    
    [Inject]
    public IStorageService StorageService { get; set; } = null!;
    
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    
    private int _amountOfNotes = 0;
    private string _token;
    private int _pageNumber = 1;
    private bool _open;
    private DrawerClipMode _clipMode = DrawerClipMode.Never;
    private List<Note> _notes = [];
    private Note? _receivedNote = new Note("","", new Guid());
    
    private string SearchText { get; set; } = string.Empty;
    private List<Note> FilteredNotes { get; set; } = [];
    private void OnSearchTextChanged(string text)
    {
        SearchText = text;

        if (string.IsNullOrWhiteSpace(SearchText))
        {
            FilteredNotes = new List<Note>(_notes);
        }
        else
        {
            FilteredNotes = _notes
                .Where(n => 
                    (n.Title != null && n.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) || 
                    (n.Body != null && n.Body.Contains(SearchText, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }
        StateHasChanged();
    }
    private void Logout()
    {
        StorageService.RemoveItemAsync("jwtToken");
        NavigationManager.NavigateTo("/");
    }
    
    protected override async Task OnInitializedAsync()
    {
        _token = await StorageService.GetItemAsync("jwtToken");
        if (_token is null or FilterOperator.String.Empty)
        {
            await StorageService.RemoveItemAsync("jwtToken");
            NavigationManager.NavigateTo("/");
        }

        try
        {
            var request = new NoteApp.Web.Contexts.NoteContext.UseCases.GetAll.Request
            {
                Token = this._token,
                PageNumber = this._pageNumber
            };
            
            var response = await HandleGetAllRequest.Handle(request, new CancellationToken());
            
            if (response.IsSuccess)
            {
                _amountOfNotes = response.Data.Notes.Count;
                _notes = response.Data.Notes;
            }
            else
            {
                await StorageService.RemoveItemAsync("jwtToken");
                NavigationManager.NavigateTo("/");
            }
            FilteredNotes = new List<Note>(_notes);
            
        }
        catch (Exception e)
        {
            // NavigationManager.NavigateTo("/");
            Console.WriteLine($"debug: {e}");
        }
    }
    
    private void ToggleDrawer()
    {
        _open = !_open;
    }
    
    private void HandleData(Note data)
    {
        _receivedNote = data;
        StateHasChanged();
    }

    private void UpdateNoteList(NoteUpdate note)
    {
        var index = _notes.FindIndex(n => n.Id == note.Id);
        _notes[index].SetTitle(note.Title);       
        _notes[index].SetBody(note.Body);
        StateHasChanged();
    }

    private void SetAmountNotes(int amount)
    {
        _amountOfNotes = amount;
        StateHasChanged();
    }

    private async void CreateNote()
    {
        var createRequest = new NoteApp.Web.Contexts.NoteContext.UseCases.Create.Request
        {
            Title = "Nova nota",
            Body = " ",
            Token = _token
        };
        var response = await HandleCreateRequest.Handle(createRequest, new CancellationToken());
    
        if (response.IsSuccess)
        {
            var note = new Note(response.Data.Title, response.Data.Body, new Guid(response.Data.UserId));
            note.CreatedAt = response.Data.CreatedAt;
            note.SetId(new Guid(response.Data.Id));
            
            _notes.Insert(0, note); 
            _amountOfNotes = _notes.Count;
            FilteredNotes = _notes;
            StateHasChanged();
        }
        
    }

    private async void LoadNotes()
    {
        this._pageNumber++;
        var request = new NoteApp.Web.Contexts.NoteContext.UseCases.GetAll.Request
        {
            Token = this._token,
            PageNumber = this._pageNumber
        };
            
        var response = await HandleGetAllRequest.Handle(request, new CancellationToken());
        
        if (response.IsSuccess)
        {
            _notes.AddRange(response.Data.Notes);
            _amountOfNotes = _notes.Count;
            FilteredNotes = _notes;
            StateHasChanged();
        }
    }
}