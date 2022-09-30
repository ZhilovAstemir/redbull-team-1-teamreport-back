using redbull_team_1_teamreport_back.Data.Entities;

namespace redbull_team_1_teamreport_back.Data.Repositories.Interfaces;
public interface IMemberRepository
{
    public Member? GetByEmail(string email);
    public int Add(Member member);
    public List<Member> GetAll();
}
