using FluentAssertions;
using TeamReport.WebAPI.Validators;

namespace TeamReport.WebAPI.Tests.Validators;

public class EditMemberInformationValidatorTest
{
    private readonly ValidatorTestFixture _fixture;
    public EditMemberInformationValidatorTest()
    {
        _fixture = new ValidatorTestFixture();
    }

    [Fact]
    public void ShouldBeAbleToCreateEditMemberInformationValidator()
    {
        var validator = new EditMemberInformationValidator();
        validator.Should().NotBeNull();
    }

    [Fact]
    public void ShouldEditMemberInformationValidatorValidateModel()
    {
        var validator = new EditMemberInformationValidator();
        var model = _fixture.GetEditMemberInformationRequest();
        var result = validator.Validate(model);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ShouldEditMemberInformationValidatorValidateId()
    {
        var validator = new EditMemberInformationValidator();
        var model = _fixture.GetEditMemberInformationRequest();
        model.Id = null;
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.Contain(error => error.PropertyName == "Id");
    }

    [Fact]
    public void ShouldEditMemberInformationValidatorValidateFirstName()
    {
        var validator = new EditMemberInformationValidator();
        var model = _fixture.GetEditMemberInformationRequest();
        model.FirstName = null;
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.Contain(error => error.PropertyName == "FirstName");
    }

    [Fact]
    public void ShouldEditMemberInformationValidatorValidateLastName()
    {
        var validator = new EditMemberInformationValidator();
        var model = _fixture.GetEditMemberInformationRequest();
        model.LastName = null;
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.Contain(error => error.PropertyName == "LastName");
    }

    [Fact]
    public void ShouldEditMemberInformationValidatorValidateTitle()
    {
        var validator = new EditMemberInformationValidator();
        var model = _fixture.GetEditMemberInformationRequest();
        model.Title = null;
        var result = validator.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.Contain(error => error.PropertyName == "Title");
    }
}