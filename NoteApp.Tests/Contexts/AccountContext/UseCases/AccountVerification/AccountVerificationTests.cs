using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using NoteApp.Domain.Services;
using NoteApp.Tests.Data;
using NoteApp.Tests.Services;

namespace NoteApp.Tests.Contexts.AccountContext.UseCases.AccountVerification;

[TestClass]
public class AccountVerificationTests
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
            .GetRequiredService<ISendEmailService>() as MockSendEmailService;
        
        urlEndpoint = "api/v1/verification";
    }

    [TestMethod]
    public async Task PostShouldReturnStatusCode400IfTheEmailIsNotInTheCorrectFormat()
    {
        var body = new { email = "lucas", code = "123456" };

        var result = await _client.PostAsJsonAsync(urlEndpoint, body);

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }
    
    [TestMethod]
    public async Task PostShouldReturnStatusCode400IfTheCodeIsNotInTheCorrectFormat()
    {
        var body = new { email = "lucas@gmail.com", code = "12345" };

        var result = await _client.PostAsJsonAsync(urlEndpoint, body);

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }
    
    [TestMethod]
    public async Task PostShouldReturnStatusCode404IfTheAccountIsNotRegistered()
    {
        var body = new { email = "antonio@gmail.com", code = "123456" };

        var result = await _client.PostAsJsonAsync(urlEndpoint, body);

        Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
    }
    
    [TestMethod]
    public async Task PostShouldReturnStatusCode400IfTheCodeIsNotValid()
    {
        _mockEmailService.Clear();
        
        var body = new { email = "antonio@gmail.com", name = "Antonio", password = "123456789@" };
        
        await _client.PostAsJsonAsync("api/v1/account", body);
        
        var email = _mockEmailService?.SentEmails[0];
        
        var request = new { email = email.To, code = "123456" };
        
        var response = await _client.PostAsJsonAsync(urlEndpoint, request);

        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [TestMethod]
    public async Task PostShouldReturnStatusCode200IfTheCodeIsValid()
    {
        _mockEmailService.Clear();
        
        var body = new { email = "lelia@gmail.com", name = "Lelia", password = "123456789@" };
        
        await _client.PostAsJsonAsync("api/v1/account", body);
        
        var email = _mockEmailService?.SentEmails[0];
        
        var request = new { email = email.To, code = email.Code };
        
        var response = await _client.PostAsJsonAsync(urlEndpoint, request);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
}