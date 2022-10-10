using AutoMapper;
using redbull_team_1_teamreport_back.Data.Repositories.Interfaces;
using TeamReport.Data.Entities;
using TeamReport.Domain.Exceptions;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services.Interfaces;

namespace TeamReport.Domain.Services;
public class ReportService: IReportService
{
    private readonly IReportRepository _reportRepository;
    private readonly IWeekRepository _weekRepository;
    private readonly IMapper _mapper;

    public ReportService(IReportRepository reportRepository, IMapper mapper, IWeekRepository weekRepository)
    {
        _reportRepository = reportRepository;
        _mapper = mapper;
        _weekRepository = weekRepository;   
    }

    public async Task<int> Add(ReportModel report, Member member)
    {
        int reportId;
        var week = await _weekRepository.GetWeekByEndDate(report.Week.DateEnd.Date);
        if (report.Week.DateEnd < report.Week.DateStart)
        {
            throw new DataException("Start date is more then end date");
        }
        if (week is null)
        {
            var newWeek = new WeekModel
            {
                DateEnd = report.Week.DateEnd,
                DateStart = report.Week.DateStart
            };
            await _weekRepository.Add(_mapper.Map<Week>(newWeek));
            reportId = await _reportRepository.Create(_mapper.Map<Report>(report), _mapper.Map<Week>(newWeek), member);
        }
        else
        { 
            reportId = await _reportRepository.Create(_mapper.Map<Report>(report), week, member);
        }
        
        return reportId;
    }
	
	public async Task<List<ReportModel>> GetReportsByMemberId(Member member)
    {
        if(member == null)
        {
            throw new DataException("member is null");
        }
        int memberId = member.Id;
        List<Report> reports = await _reportRepository.GetMemberReportsById(memberId);
        if (reports != null)
        {
            return _mapper.Map<List<ReportModel>>(reports);
        }
        return null;
    }
}
