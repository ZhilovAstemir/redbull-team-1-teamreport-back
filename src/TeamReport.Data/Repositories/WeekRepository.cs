using Microsoft.EntityFrameworkCore;
using redbull_team_1_teamreport_back.Data.Repositories.Interfaces;
using TeamReport.Data.Entities;
using TeamReport.Data.Persistence;

namespace redbull_team_1_teamreport_back.Data.Repositories;
public class WeekRepository: IWeekRepository
{
    private readonly ApplicationDbContext _context;

    public WeekRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Week> GetWeekByEndDate(DateTime endData) =>
        await _context.Weeks.FirstOrDefaultAsync(d => d.DateEnd.Date == endData.Date);

    public async Task<int> Add(Week week)
    {
        _context.Weeks.Add(week);
        await _context.SaveChangesAsync();

        return week.Id;
    }

}
