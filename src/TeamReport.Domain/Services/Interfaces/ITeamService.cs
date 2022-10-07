using TeamReport.Domain.Models;

namespace TeamReport.Domain.Services.Interfaces;
public interface ITeamService
{

    public Task<List<MemberModel>> GetMemberLeaders(int memberId);
    public Task<List<MemberModel>> GetMemberReporters(int memberId);
    public Task<List<MemberModel>> UpdateMemberLeaders(int memberId, List<int> newLeadersIds);
    public Task<List<MemberModel>> UpdateMemberReporters(int memberId, List<int> newReportersIds);
    public Task<List<MemberModel>> GetAllTeamMembers(int companyId);
    public Task<MemberModel?> GetMemberById(int id);
}
