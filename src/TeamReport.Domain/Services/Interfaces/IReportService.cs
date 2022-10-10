using TeamReport.Data.Entities;
using TeamReport.Domain.Models;

namespace TeamReport.Domain.Services.Interfaces;
public interface IReportService
{
    public Task<int> Add(ReportModel report, Member member);
}
