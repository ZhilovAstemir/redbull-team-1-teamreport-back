using FluentAssertions;
using TeamReport.WebAPI.Validators;

namespace TeamReport.WebAPI.Tests.Validators;

public class UpdateCompanyNameValidatorTest
{
    private readonly ValidatorTestFixture _fixture;

    public UpdateCompanyNameValidatorTest()
    {
        _fixture = new ValidatorTestFixture();
    }

    [Fact]
    public void ShouldBeAbleToCreateUpdateCompanyNameValidator()
    {
        var validator = new UpdateCompanyNameValidator();
        validator.Should().NotBeNull().And.BeOfType<UpdateCompanyNameValidator>();
    }

    [Fact]
    public void ShouldUpdateCompanyNameValidatorValidateCompanyNameField()
    {
        var validator = new UpdateCompanyNameValidator();
        var model = _fixture.GetUpdateCompanyNameRequest();

        var result=validator.Validate(model);
        result.IsValid.Should().BeTrue();
    }

    
    [Fact]
    public void ShouldUpdateCompanyNameValidatorValidateShortCompanyName()
    {
        var validator = new UpdateCompanyNameValidator();
        var model = _fixture.GetUpdateCompanyNameRequest();
        model.NewCompanyName = "aaa";

        var result=validator.Validate(model);
        result.IsValid.Should().BeFalse();
        result.Errors[0].PropertyName.Should().Be("NewCompanyName");
    }

    [Fact]
    public void ShouldUpdateCompanyNameValidatorValidateEmptyCompanyName()
    {
        var validator = new UpdateCompanyNameValidator();
        var model = _fixture.GetUpdateCompanyNameRequest();
        model.NewCompanyName = null;

        var result=validator.Validate(model);
        result.IsValid.Should().BeFalse();
        result.Errors[0].PropertyName.Should().Be("NewCompanyName");
    }
}