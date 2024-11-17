using System.Net.Http.Json;
using MediatR;
using NoteApp.Domain.Contexts.AccountContext.UseCases.Authenticate;

namespace NoteApp.Web.Contexts.AccountContext.UseCases.Authenticate;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly HttpClient _httpClient;

    public Handler(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient.CreateClient(Configuration.HttpClientName);
    }
    
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
       var response =  await _httpClient.PostAsJsonAsync("api/v1/authenticate", request, cancellationToken);
       var result = await response.Content.ReadFromJsonAsync<Response>();
       return result;
    }
}