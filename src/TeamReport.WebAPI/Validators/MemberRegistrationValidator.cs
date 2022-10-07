using FluentValidation;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Validators;

public class MemberRegistrationValidator : AbstractValidator<MemberRegistrationRequest>
{
    public MemberRegistrationValidator()
    {
        RuleFor(request =>
                request.Email)
            .NotEmpty().EmailAddress().WithMessage("Email is required");
        RuleFor(request =>
                request.Password)
            .NotEmpty().WithMessage("Password is required")
            .Length(6, 30).WithMessage("Password is too short")
            .Matches("(?=.*?[#?!@$%^&*-])").WithMessage("Password should contain at least 1 symbol");
        RuleFor(request =>
                request.LastName)
            .NotEmpty().WithMessage("Last Name is required");
        RuleFor(request =>
                request.FirstName)
            .NotEmpty().WithMessage("First Name is required");
        RuleFor(request =>
                request.Title)
            .NotEmpty().WithMessage("Title is required");
    }
}