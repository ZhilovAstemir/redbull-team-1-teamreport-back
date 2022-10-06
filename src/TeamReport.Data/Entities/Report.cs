using TeamReport.Data.Enums;

namespace TeamReport.Data.Entities;

public class Report
{
    public int Id { get; set; }
    public Member? Member { get; set; }

    public Emotion Morale { get; set; }
    public string? MoraleComment { get; set; }

    public Emotion Stress { get; set; }
    public string? StressComment { get; set; }

    public Emotion Workload { get; set; }
    public string? WorkloadComment { get; set; }

    public string? High { get; set; }
    public string? Low { get; set; }
    public string? Else { get; set; }

    public Week? Week { get; set; }
}