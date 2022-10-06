using TeamReport.Data.Entities;

namespace TeamReport.Data.Repositories.Interfaces
{
    public interface ILeadershipRepository : IRepository<Leadership>
    {
        public Task<List<Member>> ReadLeaders(int reporterId);
        public Task<List<Member>> ReadReporters(int leaderId);

        public Task<List<Member>> UpdateLeaders(int reporterId, List<Member> leaders);
        public Task<List<Member>> UpdateReporters(int leaderId, List<Member> reporters);

        public Task<bool> DeleteLeaders(int reporterId);
        public Task<bool> DeleteReporters(int leaderId);

        public Task<bool> DeleteLeaderships(int memberId);
    }
}