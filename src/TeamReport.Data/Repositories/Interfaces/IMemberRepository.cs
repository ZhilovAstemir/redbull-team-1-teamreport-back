using redbull_team_1_teamreport_back.Domain.Entities;

namespace redbull_team_1_teamreport_back.Domain.Repositories.Interfaces;
public interface IMemberRepository
{
    public Member GetMemberByEmail(string email);
    public int AddMember(Member member);
}
