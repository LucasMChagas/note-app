using System.Net;
using System.Net.Http.Json;
using NoteApp.Tests.Integration;

namespace NoteApp.Tests.Contexts.AccountContext.UseCases.Authenticate;

[TestClass]
public class AuthenticateTests
{
    private NoteAppApiApplication? _application;
    private HttpClient? _client;
    string urlEndpoint;

    [TestInitialize]
    public async Task Setup()
    {
        _application = new NoteAppApiApplication();

        await NoteAppMockData.CreateUsers(_application, true);

        _client = _application.CreateClient();

        urlEndpoint = "api/v1/authenticate";
    }

    [TestMethod]
    public async Task POSTGivenAnActiveUserAccountWithCorrectCredentialsItShouldReturnStatusCode200()
    {
        var body = new { email = "lumidach@gmail.com", password = "123456789@" };

        var result = await _client.PostAsJsonAsync(urlEndpoint, body);

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
    }

    [TestMethod]
    public async Task POSTGivenAnActiveUserAccountWithIncorrectCredentialsItShouldReturnStatusCode401()
    {
        var body = new { email = "lumidach@gmail.com", password = "1123456789@" };

        var result = await _client.PostAsJsonAsync(urlEndpoint, body);

        Assert.AreEqual(HttpStatusCode.Unauthorized, result.StatusCode);
    }

    [TestMethod]
    public async Task POSTGivenAnInactiveUserAccountWithCorrectCredentialsItShouldReturnStatusCode403()
    {
        var body = new { email = "lucas@gmail.com", password = "123456789@" };

        var result = await _client.PostAsJsonAsync(urlEndpoint, body);

        Assert.AreEqual(HttpStatusCode.Forbidden, result.StatusCode);
    }

    [TestMethod]
    public async Task POSTGivenAnInvalidRequestItShouldReturnStatusCode400()
    {
        var body = new { password = "123456789@" };

        var result = await _client.PostAsJsonAsync(urlEndpoint, body);

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [TestMethod]
    public async Task POSTGivenAnUnregisteredAccountItShouldReturnStatusCode404()
    {
        var body = new { email = "matheus@gmail.com", password = "123456789@" };

        var result = await _client.PostAsJsonAsync(urlEndpoint, body);

        Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
    }



}
