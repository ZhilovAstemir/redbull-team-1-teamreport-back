using redbull_team_1_teamreport_back.Data.Entities;
using TeamReport.Domain.Models;

namespace TeamReport.Domain.Services.Interfaces;
public interface IMemberService
{
    public int AddMember(MemberModel member);
    public List<Member> GetAllMembers();
}
