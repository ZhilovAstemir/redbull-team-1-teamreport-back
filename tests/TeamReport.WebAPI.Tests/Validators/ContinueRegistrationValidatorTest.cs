using FluentAssertions;
using TeamReport.WebAPI.Validators;

namespace TeamReport.WebAPI.Tests.Validators;

public class ContinueRegistrationValidatorTest
{
    private readonly ValidatorTestFixture _fixture;
    public ContinueRegistrationValidatorTest()
    {
        _fixture = new ValidatorTestFixture();
    }
    [Fact]
    public void ShouldBeAbleToCreateContinueRegistrationValidator()
    {
        var validator = new ContinueRegistrationValidator();
        validator.Should().NotBeNull().And.BeOfType<ContinueRegistrationValidator>();
    }

    [Fact]
    public void ShouldContinueRegistrationValidatorValidateModel()
    {
        var validator = new ContinueRegistrationValidator();
        var model = _fixture.GetContinueRegistrationRequest();
        var result = validator.Validate(model);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ShouldContinueRegistrationValidatorValidateTitle()
    {
        var validator = new ContinueRegistrationValidator();
        var model = _fixture.GetContinueRegistrationRequest();
        model.Title = null;
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.Contain(x => x.ErrorMessage.Contains("Title"));
    }

    [Fact]
    public void ShouldContinueRegistrationValidatorValidatePassword()
    {
        var validator = new ContinueRegistrationValidator();
        var model = _fixture.GetContinueRegistrationRequest();
        model.Password = null;
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.Contain(x => x.ErrorMessage.Contains("Password"));

        model.Password = "pass!";
        result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.Contain(x => x.ErrorMessage.Contains("Password"));
    }
}