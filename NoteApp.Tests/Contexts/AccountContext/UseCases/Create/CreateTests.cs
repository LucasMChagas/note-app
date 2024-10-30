using Microsoft.Extensions.DependencyInjection;
using NoteApp.Domain.Services;
using NoteApp.Tests.Data;
using NoteApp.Tests.Services;
using System.Net;
using System.Net.Http.Json;

namespace NoteApp.Tests.Contexts.AccountContext.UseCases.Create;

[TestClass]
public class CreateTests
{
    private NoteAppApiApplication _application = null!;
    private HttpClient _client = null!;
    string urlEndpoint = string.Empty;    

    [TestInitialize]
    public async Task Setup()
    {
        _application = new NoteAppApiApplication();

        await NoteAppMockData.CreateUsers(_application, true);

        _client = _application.CreateClient();

        urlEndpoint = "api/v1/account";
    }

    [TestMethod]
    public async Task PostCreateANewAccountMustReturnStatusCode201()
    {
        var body = new { email = "lumi@gmail.com",name = "Lucas", password = "123456789@" };

        var result = await _client.PostAsJsonAsync(urlEndpoint, body);
        
        Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
    }

    [TestMethod]
    public async Task PostCreateANewAccountWithAnEmailAlreadyInUseMustReturnStatusCode409()
    {
        var body = new { email = "lucas@gmail.com", name = "Lucas", password = "123456789@" };

        var result = await _client.PostAsJsonAsync(urlEndpoint, body);
        
        Assert.AreEqual(HttpStatusCode.Conflict, result.StatusCode);        
    }

    [TestMethod]
    public async Task PostCreateANewAccountWithAWrongRequestMustReturnStatusCode400()
    {
        var body = new { email = "lucas@gmail.com", password = "123456789@" };

        var result = await _client.PostAsJsonAsync(urlEndpoint, body);

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }


}
