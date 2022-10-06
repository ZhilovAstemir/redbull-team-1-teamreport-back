using TeamReport.Data.Entities;

namespace TeamReport.Domain.Services.Interfaces;
public interface ITeamService
{
    public Task<Member> Add(Member member);
    public Task<Member?> Get(int id);
    public Task<List<Member>> GetAll();
}
