using System.Net.Http.Headers;
using System.Net.Http.Json;
using MediatR;
using NoteApp.Domain.Contexts.NoteContext.UseCases.GetAll;

namespace NoteApp.Web.Contexts.NoteContext.UseCases.GetAll;

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
        var response =  await _httpClient.GetAsync($"api/v1/note/get-all?page={request.PageNumber}&pageSize={request.PageSize}", cancellationToken);

        Response result;
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<Response>();
        }
        else
        {
            result = new Response("Não autorizado", 401);
        }
        
        return result;
    }
}