using redbull_team_1_teamreport_back.Data.Entities;

namespace redbull_team_1_teamreport_back.Data.Repositories.Interfaces;

public interface IMemberRepository : IRepository<Member>
{
    public Task<Member?> ReadByEmail(string email);
}
