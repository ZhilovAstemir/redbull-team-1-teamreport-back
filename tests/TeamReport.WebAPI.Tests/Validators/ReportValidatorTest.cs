using FluentAssertions;
using TeamReport.WebAPI.Models;
using TeamReport.WebAPI.Validators;

namespace TeamReport.WebAPI.Tests.Validators;
public class ReportValidatorTest
{
    [Fact]
    public void ShouldBeAbleToCreateReportRequestValidator()
    {
        var validator = new ReportRequestValidator();
        validator.Should().NotBeNull().And.BeOfType<ReportRequestValidator>();
    }

    [Fact]
    public void ShouldLoginValidatorValidateMoralComment()
    {
        var validator = new ReportRequestValidator();
        var model = new ReportRequest()
        {
            MoraleComment = ""
        };
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(12).And.Contain(x => x.ErrorMessage.Contains("MoralComment"));
    }

    [Fact]
    public void ShouldLoginValidatorValidateStressComment()
    {
        var validator = new ReportRequestValidator();
        var model = new ReportRequest()
        {
            StressComment = ""
        };
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(12).And.Contain(x => x.ErrorMessage.Contains("StressComment"));
    }

    [Fact]
    public void ShouldLoginValidatorValidateWorkloadComment()
    {
        var validator = new ReportRequestValidator();
        var model = new ReportRequest()
        {
            WorkloadComment = ""
        };
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(12).And.Contain(x => x.ErrorMessage.Contains("WorkloadComment"));
    }

    [Fact]
    public void ShouldLoginValidatorValidateHigh()
    {
        var validator = new ReportRequestValidator();
        var model = new ReportRequest()
        {
            High = ""
        };
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(12).And.Contain(x => x.ErrorMessage.Contains("High"));
    }

    [Fact]
    public void ShouldLoginValidatorValidateLow()
    {
        var validator = new ReportRequestValidator();
        var model = new ReportRequest()
        {
            Low = ""
        };
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(12).And.Contain(x => x.ErrorMessage.Contains("Low"));
    }

    [Fact]
    public void ShouldLoginValidatorValidateElse()
    {
        var validator = new ReportRequestValidator();
        var model = new ReportRequest()
        {
            Else = ""
        };
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(12).And.Contain(x => x.ErrorMessage.Contains("Else"));
    }
}
