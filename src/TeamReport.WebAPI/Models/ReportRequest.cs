using TeamReport.Data.Enums;

namespace TeamReport.WebAPI.Models;

public class ReportRequest
{
    public Emotion Morale { get; set; }
    public string? MoraleComment { get; set; }
    public Emotion Stress { get; set; }
    public string? StressComment { get; set; }
    public Emotion Workload { get; set; }
    public string? WorkloadComment { get; set; }
    public string? High { get; set; }
    public string? Low { get; set; }
    public string? Else { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }            
}
