using FluentAssertions;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Enums;

namespace Data.Tests.Entities;

public class ReportTest
{
    [Fact]
    public void ShouldBeAbleToCreateReport()
    {
        var report = new Report();
        report.Should().NotBeNull();
    }

    [Fact]
    public void ShouldReportReportHaveProperties()
    {
        var member = new Member();
        var week= new Week() { DateStart = DateTime.MaxValue, DateEnd =DateTime.MaxValue };

        var report = new Report()
        {
            Id = 1,
            Member = member,
            Morale = Emotion.Great,
            MoraleComment = "MoraleComment",
            Stress = Emotion.Great,
            StressComment = "StressComment",
            Workload = Emotion.Great,
            WorkloadComment = "WorkloadComment",
            High = "High",
            Low = "Low",
            Else = "Else",
            Week = week
        };

        report.Id.Should().Be(1);
        report.Member.Should().Be(member);
        report.Morale.Should().Be(Emotion.Great);
        report.MoraleComment.Should().Be("MoraleComment");
        report.Stress.Should().Be(Emotion.Great);
        report.StressComment.Should().Be("StressComment");
        report.Workload.Should().Be(Emotion.Great);
        report.WorkloadComment.Should().Be("WorkloadComment");
        report.High.Should().Be("High");
        report.Low.Should().Be("Low");
        report.Else.Should().Be("Else");
        report.Week.Should().NotBeNull();
    }
}