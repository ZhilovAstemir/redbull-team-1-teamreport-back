using redbull_team_1_teamreport_back.Data.Entities;

namespace redbull_team_1_teamreport_back.Data.Repositories.Interfaces;

public interface IMemberRepository : IRepository<Member>
{
    public Member? ReadByEmail(string email);
}
