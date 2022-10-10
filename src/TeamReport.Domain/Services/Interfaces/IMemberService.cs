using TeamReport.Domain.Models;

namespace TeamReport.Domain.Services.Interfaces;
public interface IMemberService
{
    public Task<MemberModel> Register(MemberModel member);
    public Task<List<MemberModel>> GetAll();
    public Task<string> GetToken(MemberModel member);
    public Task<MemberModel> Login(string email, string password);
    public Task<MemberModel> ContinueRegistration(MemberModel memberModel);
    public Task<MemberModel> GetMemberByEmail(string email);
    public Task<MemberModel?> GetMemberById(int id);
    public Task<MemberModel> UpdateMemberInformationBeforeInvite(MemberModel model);
    public Task<MemberModel> EditMemberInformation(MemberModel model);
}
