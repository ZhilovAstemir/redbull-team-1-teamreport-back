using TeamReport.Data.Entities;

namespace redbull_team_1_teamreport_back.Data.Repositories.Interfaces;
public interface IReportRepository
{
    public Task<List<Report>> GetMemberReportsById(int memberId);
    public Task<int> Create(Report report, Week week, Member member);
}

