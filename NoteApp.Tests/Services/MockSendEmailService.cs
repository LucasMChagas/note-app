using NoteApp.Domain.Contexts.AccountContext.Entities;
using NoteApp.Domain.Services;

namespace NoteApp.Tests.Services
{
    public class MockSendEmailService : ISendEmailService
    {
        public List<EmailMessage> SentEmails { get; } = new List<EmailMessage>();
        public bool ShouldFail {get; set;} = false;

        public Task SendVerificatioEmailAsync(User user, CancellationToken cancellationToken)
        {
            if (ShouldFail)
            {
                throw new Exception("Simulated email send failure");
            }

            var emailMessage = new EmailMessage(user.Email.Address, "Verifique sua conta", user.Email.Verification.Code);
            SentEmails.Add(emailMessage);

            return Task.CompletedTask;
        }

        public void Clear()
        {
            SentEmails.Clear();
        }
    }    
    public record EmailMessage(string To, string Subject, string Code);
}
