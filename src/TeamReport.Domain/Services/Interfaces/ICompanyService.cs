using TeamReport.Domain.Models;

namespace TeamReport.Domain.Services.Interfaces;

public interface ICompanyService
{
    public Task<CompanyModel?> GetCompany(int memberId);

    public Task<CompanyModel?> SetName(int memberId, string newCompanyName);

    public Task<MemberModel?> Register(MemberModel memberModel);
}