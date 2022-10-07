using TeamReport.Domain.Models;

namespace TeamReport.Domain.Services.Interfaces;

public interface IEmailService
{
    bool InviteMember(MemberModel memberModel, string frontDomain);
}