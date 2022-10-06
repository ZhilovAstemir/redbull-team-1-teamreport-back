using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using TeamReport.Domain.Infrastructures;
using TeamReport.Domain.Models;

namespace TeamReport.Domain.Services;
public class EmailService : IEmailService
{
    private readonly EmailConfiguration _emailConfig;

    public EmailService(IOptions<EmailConfiguration> emailConfig)
    {
        _emailConfig = emailConfig.Value;
    }

    public void InviteMember(InviteMemberRequest request, string path)
    {
        var message = new Message(new string[]
           { request.Email },
           "Invitation",
           $"Dear {request.FirstName} {request.LastName} we invite you {path}");
        var emailMessage = CreateEmailMessage(message);

        Send(emailMessage);
    }

    private MimeMessage CreateEmailMessage(Message message)
    {
        var emailMessage = new MimeMessage();
        var address = new MailboxAddress(_emailConfig.UserName, _emailConfig.From);
        emailMessage.From.Add(address);
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

        return emailMessage;
    }

    private void Send(MimeMessage mailMessage)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                client.Send(mailMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
