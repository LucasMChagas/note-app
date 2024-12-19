using System.Net.Http.Headers;
using System.Net.Http.Json;
using MediatR;
using NoteApp.Domain.Contexts.NoteContext.UseCases.Create;

namespace NoteApp.Web.Contexts.NoteContext.UseCases.Create;

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
        var response =  await _httpClient.PostAsJsonAsync($"api/v1/note",request, cancellationToken);
        var result = await response.Content.ReadFromJsonAsync<Response>();
        return result;
    }
}