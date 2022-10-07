using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using TeamReport.Domain.Infrastructures;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services.Interfaces;

namespace TeamReport.Domain.Services;
public class EmailService : IEmailService
{
    private readonly EmailConfiguration _emailConfig;

    public EmailService(IOptions<EmailConfiguration> emailConfig)
    {
        _emailConfig = emailConfig.Value;
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

    public bool InviteMember(MemberModel memberModel, string frontDomain)
    {
        var token = GetToken(memberModel);
        var link = frontDomain + "?token=" + token;
        var message = new Message(new string[]
                { memberModel.Email },
            "Invitation",
            $"Dear {memberModel.FirstName} {memberModel.LastName},\r\n We invite you join our team. Please click to continue registration: {link}");
        var emailMessage = CreateEmailMessage(message);

        Send(emailMessage);
        return true;
    }

    public string GetToken(MemberModel member)
    {
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256),
            claims: new List<Claim>() { new Claim("user", member.Id.ToString()) });

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
