using redbull_team_1_teamreport_back.Data.Entities;
using TeamReport.Domain.Models;

namespace TeamReport.Domain.Services.Interfaces;
public interface IMemberService
{
    public Task<Member> Register(MemberModel member);
    public Task<List<Member>> GetAll();
    public Task<string> GetToken(MemberModel member);

    public Task<MemberModel> Login(string email, string password);

}
