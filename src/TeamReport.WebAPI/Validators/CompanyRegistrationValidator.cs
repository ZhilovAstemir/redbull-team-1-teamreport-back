using FluentValidation;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Validators;

public class CompanyRegistrationValidator : AbstractValidator<CompanyRegistrationRequest>
{
    public CompanyRegistrationValidator()
    {
        RuleFor(request => request.Email).NotEmpty().EmailAddress().WithMessage("Email is invalid");
        RuleFor(x => x.Company).NotEmpty().WithMessage("Company is required");
        RuleFor(x => x.Company.Name).NotEmpty().Length(4, 100).WithMessage("Company Name is too short or empty");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .Length(6, 30).WithMessage("Password is too short")
            .Matches("(?=.*?[#?!@$%^&*-])").WithMessage("Password should contain at least 1 symbol");
    }
}