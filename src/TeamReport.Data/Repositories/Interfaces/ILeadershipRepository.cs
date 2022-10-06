using TeamReport.Data.Entities;

namespace TeamReport.Data.Repositories.Interfaces;

public interface ILeadershipRepository : IRepository<Leadership>
{
    public Task<List<Member>> ReadLeaders(int memberId);
    public Task<List<Member>> ReadReporters(int leaderId);
    public Task<bool> DeleteLeaderships(int memberId);
}