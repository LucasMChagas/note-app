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
    private MockSendEmailService? _mockEmailService;
    private HttpClient _client = null!;
    string urlEndpoint = string.Empty;    

    [TestInitialize]
    public async Task Setup()
    {
        _application = new NoteAppApiApplication();

        await NoteAppMockData.CreateUsers(_application, true);

        _client = _application.CreateClient();

        _mockEmailService = _application
            .Services
            .CreateScope().ServiceProvider
            .GetRequiredService<ISendEmailService>() as MockSendEmailService;  

        urlEndpoint = "api/v1/account";
    }

    [TestMethod]
    public async Task POSTCreateANewAccountMustReturnStatusCode201()
    {
        _mockEmailService?.Clear();

        var body = new { email = "lumi@gmail.com",name = "Lucas", password = "123456789@" };

        var result = await _client.PostAsJsonAsync(urlEndpoint, body);

        var email = _mockEmailService?.SentEmails[0];
        Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        Assert.IsTrue(_mockEmailService?.SentEmails.Count > 0);
        Assert.AreEqual(body.email, email.To);
        Assert.IsNotNull(email.Code);
    }

    [TestMethod]
    public async Task POSTCreateANewAccountWithAnEmailAlreadyInUseMustReturnStatusCode409()
    {
        _mockEmailService?.Clear();

        var body = new { email = "lucas@gmail.com", name = "Lucas", password = "123456789@" };

        var result = await _client.PostAsJsonAsync(urlEndpoint, body);
        
        Assert.AreEqual(HttpStatusCode.Conflict, result.StatusCode);        
    }

    [TestMethod]
    public async Task POSTCreateANewAccountWithAWrongRequestMustReturnStatusCode400()
    {
        _mockEmailService?.Clear();

        var body = new { email = "lucas@gmail.com", password = "123456789@" };

        var result = await _client.PostAsJsonAsync(urlEndpoint, body);

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }


}
