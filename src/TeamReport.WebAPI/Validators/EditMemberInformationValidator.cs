using FluentValidation;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Validators;

public class EditMemberInformationValidator : AbstractValidator<EditMemberInformationRequest>
{
    public EditMemberInformationValidator()
    {
        RuleFor(request => request.Id).NotEmpty().WithMessage("Member Id is required");
        RuleFor(request => request.FirstName).NotEmpty().WithMessage("First Name is required");
        RuleFor(request => request.LastName).NotEmpty().WithMessage("Last Name is required");
        RuleFor(request => request.Title).NotEmpty().WithMessage("Title is required");
    }
}