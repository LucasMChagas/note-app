using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using NoteApp.Domain.Contexts.AccountContext.UseCases.Authenticate;
using NoteApp.Web.Services;

namespace NoteApp.Web.Pages.Contexts.AccountContext.UseCases;

public partial class AuthenticatePage : ComponentBase
{
    #region Dependecies
    
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    public IRequestHandler<Request, Response> HandleRequest { get; set; } = null!;
    [Inject]
    public IStorageService StorageService { get; set; } = null!;
    
    #endregion

    #region Properties

    public bool IsBusy { get; set; } = false;
    public Request InputModel { get; set; } = new(String.Empty, String.Empty);

    #endregion
    
    protected override Task OnInitializedAsync()
    {
        StateHasChanged();
        return Task.CompletedTask;
    }

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await HandleRequest.Handle(InputModel, new CancellationToken());
            if (result.IsSuccess)
            {
                Snackbar.Add("Login efetuado com sucesso!", Severity.Success);
                await StorageService.SetItemAsync("jwtToken", result.Data.Token);
                await StorageService.SetItemAsync("name", result.Data.Name);
                NavigationManager.NavigateTo("/notes");
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
        finally
        {
            IsBusy = false;
        }
    }
}