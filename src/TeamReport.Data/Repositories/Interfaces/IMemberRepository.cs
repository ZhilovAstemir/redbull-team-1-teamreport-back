using redbull_team_1_teamreport_back.Domain.Entities;

namespace redbull_team_1_teamreport_back.Domain.Repositories.Interfaces;
public interface IMemberRepository
{
    public Task<Member?> GetMemberByEmail(string email);
    public Task<int> AddMember(Member member);
}
