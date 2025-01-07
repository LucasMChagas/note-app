using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using NoteApp.Domain.Contexts.AccountContext.UseCases.Create;
using NoteApp.Web.Services;

namespace NoteApp.Web.Pages.Contexts.AccountContext.UseCases;

public partial class CreatePage : ComponentBase
{
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    public IRequestHandler<Request, Response> HandleRequest { get; set; } = null!;
    [Inject]
    public IStorageService StorageService { get; set; } = null!;
    
    public Request InputModel { get; set; } = new(String.Empty, String.Empty, String.Empty);
    
    public async Task OnValidSubmitAsync()
    {
        try
        {
            var result = await HandleRequest.Handle(InputModel, new CancellationToken());
            if (result.IsSuccess)
            {
                Snackbar.Add("Conta criada com sucesso!", Severity.Success); 
                NavigationManager.NavigateTo("/login");
            }
            else
            {
                Snackbar.Add(result.Message, Severity.Error);
            }
            
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
        
    }
}