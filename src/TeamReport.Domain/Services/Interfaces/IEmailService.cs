using TeamReport.Domain.Models;

namespace TeamReport.Domain.Services;

public interface IEmailService
{
    void InviteMember(InviteMemberRequest inviteMember);
}