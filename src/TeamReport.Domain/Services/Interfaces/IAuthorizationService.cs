using TeamReport.Domain.Models;

namespace TeamReport.Domain.Services.Interfaces;
public interface IAuthorizationService
{
    public MemberModel Login(string email, string password);
    public string GetToken(MemberModel member);
    public int Register(MemberModel member);
}
