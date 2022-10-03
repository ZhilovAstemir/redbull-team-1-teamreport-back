using TeamReport.Domain.Models;

namespace TeamReport.Domain.Services.Interfaces;
public interface IAuthorizationService
{
    public Task<MemberModel> Login(string email, string password);
    public Task<string> GetToken(MemberModel member);
    public Task<int> Register(MemberModel member);
}
