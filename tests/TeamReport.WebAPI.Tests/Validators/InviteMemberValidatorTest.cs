using FluentAssertions;
using TeamReport.WebAPI.Models;
using TeamReport.WebAPI.Validators;

namespace TeamReport.WebAPI.Tests.Validators;
public class InviteMemberValidatorTest
{
    [Fact]
    public void ShouldInviteMemberValidatorValidateEmail()
    {
        var validator = new InviteMemberValidator();
        var model = new InviteMemberModelRequest()
        {
            FirstName = "Ivan",
            LastName = "Ivanov",
            Email = "email",
            CurrentUserId = 1
        };
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.Contain(x => x.ErrorMessage.Contains("email"));
    }

    [Fact]
    public void ShouldInviteMemberValidatorValidateFirstName()
    {
        var validator = new InviteMemberValidator();
        var model = new InviteMemberModelRequest()
        {
            FirstName = "",
            LastName = "Ivanov",
            Email = "email@mail.ru",
            CurrentUserId = 1
        };
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(2).And.Contain(x => x.PropertyName == "FirstName");
    }

    [Fact]
    public void ShouldInviteMemberValidatorValidateLastName()
    {
        var validator = new InviteMemberValidator();
        var model = new InviteMemberModelRequest()
        {
            FirstName = "Ivan",
            LastName = "",
            Email = "email@mail.ru",
            CurrentUserId = 1
        };
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.PropertyName == "LastName");
    }

    [Fact]
    public void ShouldInviteMemberValidatorValidateModel()
    {
        var validator = new InviteMemberValidator();
        var model = new InviteMemberModelRequest()
        {
            FirstName = "Ivan",
            LastName = "LastName",
            Email = "email@mail.ru",
            CurrentUserId = 1
        };
        var result = validator.Validate(model);

        result.IsValid.Should().BeTrue();
    }
}
