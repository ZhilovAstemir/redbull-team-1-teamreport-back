using FluentValidation;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Validators;

public class InviteMemberValidator : AbstractValidator<InviteMemberModelRequest>
{
    public InviteMemberValidator()
    {
        RuleFor(i => i.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .Length(1, 100).WithMessage("Invalid first name length");
        RuleFor(i => i.LastName)
           .NotEmpty().WithMessage("Last name is required")
           .Length(1, 100).WithMessage("Invalid last name length");
        RuleFor(i => i.Email)
           .NotEmpty().WithMessage("Email is required")
           .Length(1, 50).WithMessage("Invalid email length")
           .EmailAddress().WithMessage("Invalid email format");
        RuleFor(i => i.CurrentUserId)
            .NotEmpty().WithMessage("Current User Id is required");
    }
}
