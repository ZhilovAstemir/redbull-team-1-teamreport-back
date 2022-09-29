using TeamReport.Domain.Models;

namespace TeamReport.Domain.Services.Interfaces;
public interface IAuthorizationServices
{
    public MemberModel GetUserForLogin(string email, string password);
    public string GetToken(MemberModel member);
}
