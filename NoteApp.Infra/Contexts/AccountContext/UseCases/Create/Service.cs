using Microsoft.AspNetCore.Http;
using NoteApp.Domain;
using NoteApp.Domain.Contexts.AccountContext.Entities;
using NoteApp.Domain.Contexts.AccountContext.UseCases.Create.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NoteApp.Infra.Contexts.AccountContext.UseCases.Create;
public class Service : IService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Service(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task SendVerificatioEmailAsync(User user, CancellationToken cancellationToken)
    {
        var client = new SendGridClient(Configuration.SendGrid.ApiKey);
        var from = new EmailAddress(Configuration.Email.DefaultFromEmail, Configuration.Email.DefaultFromName);
        string subject = "Verifique sua conta";
        var to = new EmailAddress(user.Email.Address, user.Name);

        var request = _httpContextAccessor.HttpContext.Request;
        var content = $"<h3>Conta cadastrada com sucesso!</h3>" +
            $"<a href=\"{request.Scheme}://{request.Host}/index.html" +
            $"?code={user.Email.Verification.Code}&email={user.Email.Address}\">" +
            $"Ativar a conta</a>";       
            
        var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
        await client.SendEmailAsync(msg, cancellationToken);
    }
}

