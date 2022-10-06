using FluentValidation;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Validators;

public class ContinueRegistrationValidator : AbstractValidator<ContinueRegistrationRequest>
{
    public ContinueRegistrationValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.Password).NotEmpty().Matches("(?=.*?[A-Za-z])(?=.*?[#?!@$%^&*-]).{5,}")
            .WithMessage("Password should contain at least 5 letters and 1 symbol");
    }
}