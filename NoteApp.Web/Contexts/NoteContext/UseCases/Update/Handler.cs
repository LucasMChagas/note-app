using System.Net.Http.Headers;
using System.Net.Http.Json;
using MediatR;
using NoteApp.Domain.Contexts.NoteContext.UseCases.Update;

namespace NoteApp.Web.Contexts.NoteContext.UseCases.Update;

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
        var response =  await _httpClient.PutAsJsonAsync($"api/v1/note/update/{request.NoteId}", request, cancellationToken);
        
        if (response.IsSuccessStatusCode)
        {
            Domain.Contexts.NoteContext.UseCases.Update.Response result;
            result = await response.Content.ReadFromJsonAsync<Domain.Contexts.NoteContext.UseCases.Update.Response>();
            return result;
        }

        return new Response("Erro", 400);
    }
}