using FluentValidation;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Validators;

public class InviteMemberValidator: AbstractValidator<InviteMemberModelRequest>
{
    public InviteMemberValidator()
    {
        RuleFor(i  => i.FirstName)
            .NotEmpty().WithMessage("First name is requared")
            .Length(1, 100).WithMessage("Invalid first name length");
        RuleFor(i => i.LastName)
           .NotEmpty().WithMessage("Last name is requared")
           .Length(1, 100).WithMessage("Invalid last name length");
        RuleFor(i => i.Email)
           .NotEmpty().WithMessage("Email is requared")
           .Length(1, 50).WithMessage("Invalid email length")
           .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Invalid email format");
    }
}
