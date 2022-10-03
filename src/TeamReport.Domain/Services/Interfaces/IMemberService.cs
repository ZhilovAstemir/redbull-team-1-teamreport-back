using TeamReport.Domain.Models;

namespace TeamReport.Domain.Services.Interfaces;
public interface IMemberService
{
    public Task<int> AddMember(MemberModel member);
}
