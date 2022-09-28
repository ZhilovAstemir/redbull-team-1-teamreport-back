using FluentAssertions;
using redbull_team_1_teamreport_back.Domain.Entities;
using redbull_team_1_teamreport_back.Domain.Enums;

namespace Domain.UnitTests.Entities;

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
        var report = new Report();

        report.Id = 1;
        var member = new Member();
        report.Member = member;
        report.Morale = Emotion.Great;
        report.MoraleComment = "MoraleComment";
        report.Stress = Emotion.Great;
        report.StressComment = "StressComment";
        report.Workload = Emotion.Great;
        report.WorkloadComment = "WorkloadComment";
        report.High = "High";
        report.Low = "Low";
        report.Else = "Else";
        report.Week = new Week() { DateStart = DateTime.MaxValue, DateEnd =DateTime.MaxValue };

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