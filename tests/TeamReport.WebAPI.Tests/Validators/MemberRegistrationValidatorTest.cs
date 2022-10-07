using FluentAssertions;
using TeamReport.WebAPI.Models;
using TeamReport.WebAPI.Validators;

namespace TeamReport.WebAPI.Tests.Validators;

public class MemberRegistrationValidatorTest
{
    [Fact]
    public void ShouldBeAbleToCreateMemberRegistrationValidator()
    {
        var validator = new MemberRegistrationValidator();
        validator.Should().NotBeNull().And.BeOfType<MemberRegistrationValidator>();
    }

    [Fact]
    public void ShouldMemberRegistrationValidatorValidateEmptyModel()
    {
        var validator = new MemberRegistrationValidator();
        var model = new MemberRegistrationRequest();
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(5);
    }

    [Fact]
    public void ShouldMemberRegistrationValidatorValidateCorrectModel()
    {
        var validator = new MemberRegistrationValidator();
        var model = new MemberRegistrationRequest()
        {
            Email = "email@email.email",
            Password = "Some password for test!",
            FirstName = "FirstName",
            LastName = "LastName",
            Title = "Title"
        };
        var result = validator.Validate(model);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ShouldMemberRegistrationValidatorValidateEmail()
    {
        var validator = new MemberRegistrationValidator();
        var model = new MemberRegistrationRequest()
        {
            Password = "Some password for test!",
            FirstName = "FirstName",
            LastName = "LastName",
            Title = "Title"
        };
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.Contain(x => x.ErrorMessage.Contains("Email"));
    }

    [Fact]
    public void ShouldMemberRegistrationValidatorValidatePassword()
    {
        var validator = new MemberRegistrationValidator();
        var model = new MemberRegistrationRequest()
        {
            Email = "email@email.email",
            Password = "pass!",
            FirstName = "FirstName",
            LastName = "LastName",
            Title = "Title"
        };
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.Contain(x => x.ErrorMessage.Contains("Password"));
    }

    [Fact]
    public void ShouldMemberRegistrationValidatorValidateFirstName()
    {
        var validator = new MemberRegistrationValidator();
        var model = new MemberRegistrationRequest()
        {
            Email = "email@email.email",
            Password = "Some password for test!",
            LastName = "LastName",
            Title = "Title"
        };
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.Contain(x => x.ErrorMessage.Contains("First Name"));
    }

    [Fact]
    public void ShouldMemberRegistrationValidatorValidateLastName()
    {
        var validator = new MemberRegistrationValidator();
        var model = new MemberRegistrationRequest()
        {
            Email = "email@email.email",
            Password = "Some password for test!",
            FirstName = "FirstName",
            Title = "Title"
        };
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.Contain(x => x.ErrorMessage.Contains("Last Name"));
    }

    [Fact]
    public void ShouldMemberRegistrationValidatorValidateTitle()
    {
        var validator = new MemberRegistrationValidator();
        var model = new MemberRegistrationRequest()
        {
            Email = "email@email.email",
            Password = "Some password for test!",
            FirstName = "FirstName",
            LastName = "LastName"
        };
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.Contain(x => x.ErrorMessage.Contains("Title"));
    }
}