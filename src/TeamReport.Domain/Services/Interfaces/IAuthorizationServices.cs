using TeamReport.Domain.Models;

namespace TeamReport.Domain.Services.Interfaces;
public interface IAuthorizationServices
{
    public Task<MemberModel> GetUserForLogin(string email, string password);
    public Task<string> GetToken(MemberModel member);
}
