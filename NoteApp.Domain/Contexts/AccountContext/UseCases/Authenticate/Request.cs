using MediatR;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.Authenticate;

public class Request : IRequest<Response>
{
    public Request()
    {
    }
    public Request(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string? Email { get; set; }
    public string? Password { get; set; }
};

