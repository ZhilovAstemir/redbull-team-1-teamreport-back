using Microsoft.EntityFrameworkCore;
﻿using redbull_team_1_teamreport_back.Data.Repositories.Interfaces;
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
    public async Task<List<Report>> GetReportsByWeekId(int weekId)
    {
        return await _context.Reports.Include(x => x.Week).Where(x => x.Week.Id == weekId).ToListAsync();
    }

    public async Task<List<Report>> GetMemberReportsById(int memberId)
    {
        var reports = _context.Reports.Include(r => r.Member).Include(r => r.Week);
        var reportsByMemberId = reports.Include(x => x.Member).Where(x => x.Member.Id == memberId).ToListAsync();

        return await reportsByMemberId;
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
