using redbull_team_1_teamreport_back.Data.Repositories.Interfaces;
using TeamReport.Data.Entities;
using TeamReport.Data.Persistence;

namespace redbull_team_1_teamreport_back.Data.Repositories;
public class ReportRepository: IReportRepository
{
    private readonly ApplicationDbContext _context;

    public ReportRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Create(Report report, Week week, Member member)
    {
        report.Week = week;
        report.Member = member;
        _context.Reports.Add(report);
        await _context.SaveChangesAsync();

        return report.Id;
    }
}
