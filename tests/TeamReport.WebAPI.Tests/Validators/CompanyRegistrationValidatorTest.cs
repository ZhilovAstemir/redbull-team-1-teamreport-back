using FluentAssertions;
using TeamReport.WebAPI.Validators;

namespace TeamReport.WebAPI.Tests.Validators;

public class CompanyRegistrationValidatorTest
{
    private readonly ValidatorTestFixture _fixture;
    public CompanyRegistrationValidatorTest()
    {
        _fixture = new ValidatorTestFixture();
    }
    [Fact]
    public void ShouldBeAbleToCreateCompanyRegistrationValidator()
    {
        var validator = new CompanyRegistrationValidator();
        validator.Should().NotBeNull().And.BeOfType<CompanyRegistrationValidator>();
    }

    [Fact]
    public void ShouldCompanyRegistrationValidatorValidateModel()
    {
        var validator = new CompanyRegistrationValidator();
        var model = _fixture.GetCompanyRegistrationRequest();
        var result = validator.Validate(model);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ShouldCompanyRegistrationValidatorValidateEmail()
    {
        var validator = new CompanyRegistrationValidator();
        var model = _fixture.GetCompanyRegistrationRequest();
        model.Email = "invalidEmail";
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.Contain(x => x.ErrorMessage.Contains("Email"));
    }

    [Fact]
    public void ShouldCompanyRegistrationValidatorValidateShortPassword()
    {
        var validator = new CompanyRegistrationValidator();
        var model = _fixture.GetCompanyRegistrationRequest();
        model.Password = "pass!";
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.Contain(x => x.ErrorMessage.Contains("Password"));

    }

    [Fact]
    public void ShouldCompanyRegistrationValidatorValidatePasswordWithoutSymbol()
    {
        var validator = new CompanyRegistrationValidator();
        var model = _fixture.GetCompanyRegistrationRequest();

        model.Password = "password";
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.Contain(x => x.ErrorMessage.Contains("Password"));

    }

    [Fact]
    public void ShouldCompanyRegistrationValidatorValidateCorrectPassword()
    {
        var validator = new CompanyRegistrationValidator();
        var model = _fixture.GetCompanyRegistrationRequest();

        model.Password = "password!";
        var result = validator.Validate(model);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ShouldCompanyRegistrationValidatorValidateRequiredCompanyName()
    {
        var validator = new CompanyRegistrationValidator();
        var model = _fixture.GetCompanyRegistrationRequest();

        model.Company.Name = null;
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors[0].PropertyName.Should().Be("Company.Name");
    }

    [Fact]
    public void ShouldCompanyRegistrationValidatorValidateShortCompanyName()
    {
        var validator = new CompanyRegistrationValidator();
        var model = _fixture.GetCompanyRegistrationRequest();

        model.Company.Name = "cm";
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors[0].PropertyName.Should().Be("Company.Name");
    }

    [Fact]
    public void ShouldCompanyRegistrationValidatorValidateRequiredFirstName()
    {
        var validator = new CompanyRegistrationValidator();
        var model = _fixture.GetCompanyRegistrationRequest();

        model.FirstName = null;
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors[0].PropertyName.Should().Be("FirstName");
    }

    [Fact]
    public void ShouldCompanyRegistrationValidatorValidateRequiredLastName()
    {
        var validator = new CompanyRegistrationValidator();
        var model = _fixture.GetCompanyRegistrationRequest();

        model.LastName = null;
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors[0].PropertyName.Should().Be("LastName");
    }

    [Fact]
    public void ShouldCompanyRegistrationValidatorValidateRequiredTitle()
    {
        var validator = new CompanyRegistrationValidator();
        var model = _fixture.GetCompanyRegistrationRequest();

        model.Title = null;
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors[0].PropertyName.Should().Be("Title");
    }
}