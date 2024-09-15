using NoteApp.Domain;
using NoteApp.Domain.Contexts.AccountContext.Entities;
using NoteApp.Domain.Contexts.AccountContext.UseCases.Create.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NoteApp.Infra.Contexts.AccountContext.UseCases.Create;
public class Service : IService
{
    public async Task SendVerificatioEmailAsync(User user, CancellationToken cancellationToken)
    {
        var client = new SendGridClient(Configuration.SendGrid.ApiKey);
        var from = new EmailAddress(Configuration.Email.DefaultFromEmail, Configuration.Email.DefaultFromName);
        string subject = "Verifique sua conta";
        var to = new EmailAddress(user.Email.Address, user.Name);
        var content = $"Código de verificação: {user.Email.Verification.Code}";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
        await client.SendEmailAsync(msg, cancellationToken);
    }
}

