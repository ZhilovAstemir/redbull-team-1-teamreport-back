using redbull_team_1_teamreport_back.Data.Entities;

namespace redbull_team_1_teamreport_back.Data.Repositories.Interfaces;
public interface IMemberRepository
{
    public Task<Member?> GetMemberByEmail(string email);
    public Task<int> AddMember(Member member);
    public List<Member> GetAll();
}
