using redbull_team_1_teamreport_back.Data.Entities;
using TeamReport.Domain.Models;

namespace TeamReport.Domain.Services.Interfaces;
public interface ITeamService
{
    public Member Add(Member member);
    public Member? Get(int id);
    public List<Member> GetAll();
}
