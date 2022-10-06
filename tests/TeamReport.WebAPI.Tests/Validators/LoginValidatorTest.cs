using FluentAssertions;
using TeamReport.WebAPI.Models;
using TeamReport.WebAPI.Validators;

namespace TeamReport.WebAPI.Tests.Validators;

public class LoginValidatorTest
{
    [Fact]
    public void ShouldBeAbleToCreateLoginValidator()
    {
        var validator = new LoginValidator();
        validator.Should().NotBeNull().And.BeOfType<LoginValidator>();
    }

    [Fact]
    public void ShouldLoginValidatorValidateEmptyModel()
    {
        var validator = new LoginValidator();
        var model = new LoginRequest();
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(2);
    }

    [Fact]
    public void ShouldLoginValidatorValidateEmail()
    {
        var validator = new LoginValidator();
        var model = new LoginRequest()
        {
            Password = "Some password for test"
        };
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.Contain(x => x.ErrorMessage.Contains("Email"));
    }

    [Fact]
    public void ShouldLoginValidatorValidatePassword()
    {
        var validator = new LoginValidator();
        var model = new LoginRequest()
        {
            Email = "email@email.email"
        };
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.Contain(x => x.ErrorMessage.Contains("Password"));
    }
}