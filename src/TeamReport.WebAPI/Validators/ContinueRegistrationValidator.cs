using FluentValidation;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Validators;

public class ContinueRegistrationValidator : AbstractValidator<ContinueRegistrationRequest>
{
    public ContinueRegistrationValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .Length(6, 30).WithMessage("Password is too short")
            .Matches("(?=.*?[#?!@$%^&*-])").WithMessage("Password should contain at least 1 symbol");
    }
}