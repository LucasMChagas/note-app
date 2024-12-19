using System.Net.Http.Headers;
using System.Net.Http.Json;
using MediatR;
using NoteApp.Domain.Contexts.NoteContext.UseCases.Delete;

namespace NoteApp.Web.Contexts.NoteContext.UseCases.Delete;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly HttpClient _httpClient;
    
    public Handler(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient.CreateClient(Configuration.HttpClientName);
    }
    
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token);
        var response =  await _httpClient.DeleteAsync($"api/v1/note/delete/{request.NoteId}", cancellationToken);

        Response result;
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<Response>();
        }
        else
        {
            result = await response.Content.ReadFromJsonAsync<Response>();
        }
        
        return result;
    }
}