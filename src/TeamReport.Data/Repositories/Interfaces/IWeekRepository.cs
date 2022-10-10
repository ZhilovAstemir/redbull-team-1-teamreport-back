using TeamReport.Data.Entities;

namespace redbull_team_1_teamreport_back.Data.Repositories.Interfaces;
public interface IWeekRepository
{
    public Task<int> Add(Week week);
    public Task<Week> GetWeekByEndDate(DateTime endData);
}
