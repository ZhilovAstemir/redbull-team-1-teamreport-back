using redbull_team_1_teamreport_back.Data.Entities;
using TeamReport.Domain.Models;

namespace TeamReport.Domain.Services.Interfaces;

public interface ICompanyService
{
    public Task<Company?> GetCompany(int memberId);

    public Task<Company?> SetName(int memberId, string newCompanyName);

    public Task<Member?> Register(MemberModel memberModel);
}