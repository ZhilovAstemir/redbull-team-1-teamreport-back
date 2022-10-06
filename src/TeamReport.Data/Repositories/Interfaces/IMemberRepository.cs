using TeamReport.Data.Entities;

namespace TeamReport.Data.Repositories.Interfaces
{
    public interface IMemberRepository : IRepository<Member>
    {
        public Task<Member?> ReadByEmail(string email);
        public Task<List<Member>> ReadByCompany(int companyId);
    }
}
