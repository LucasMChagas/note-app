using Microsoft.Extensions.DependencyInjection;
using NoteApp.Domain.Services;
using NoteApp.Tests.Data;
using NoteApp.Tests.Services;
using System.Net;
using System.Net.Http.Json;

namespace NoteApp.Tests.Contexts.AccountContext.UseCases.SendVerificationCode;
[TestClass]
public class SendVerificationCodeTests
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

        urlEndpoint = "api/v1/resend-email-validation";
    }
    [TestMethod]
    public async Task POSTShouldReturnStatusCode400IfTheEmailIsNotInTheCorrectFormat()
    {
        var body = new { email = "lucas" };

        var result = await _client.PostAsJsonAsync(urlEndpoint, body);

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [TestMethod]
    public async Task POSTItShouldReturnStatusCode200IfTheEmailIsRegisteredAndNotActivatedInTheDatabase()
    {
        var body = new { email = "lucas@gmail.com" };

        var result = await _client.PostAsJsonAsync(urlEndpoint, body);

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
    }

    [TestMethod]
    public async Task POSTItShouldReturnStatusCode404IfTheEmailIsNotRegisteredInTheDatabase()
    {
        var body = new { email = "lucas_as@gmail.com" };

        var result = await _client.PostAsJsonAsync(urlEndpoint, body);

        Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
    }

    [TestMethod]
    public async Task POSTItShouldReturnStatusCode400IfTheEmailIsAlreadyActivated()
    {
        var body = new { email = "lucasadm@gmail.com" };

        var result = await _client.PostAsJsonAsync(urlEndpoint, body);

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }
}


