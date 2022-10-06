using TeamReport.Domain.Models;

namespace TeamReport.Domain.Services.Interfaces;
public interface ITeamService
{

    public Task<List<MemberModel>> GetMemberLeaders(int memberId);
    public Task<List<MemberModel>> GetMemberReporters(int memberId);

    public Task<List<MemberModel>> UpdateMemberLeaders(MemberModel member, List<MemberModel> newLeadersModels);
    public Task<List<MemberModel>> UpdateMemberReporters(MemberModel member, List<MemberModel> newReportersModels);

    public Task<List<MemberModel>> GetAllTeamMembers(int companyId);
}
