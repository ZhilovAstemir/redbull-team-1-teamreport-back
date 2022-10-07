using FluentValidation;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Validators;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(loginRequest =>
        loginRequest.Email)
            .NotEmpty().EmailAddress().WithMessage("Email is required");
        RuleFor(loginRequest =>
        loginRequest.Password)
            .NotEmpty().WithMessage("Password is required")
            .Length(6, 30).WithMessage("Password is too short")
            .Matches("(?=.*?[#?!@$%^&*-])").WithMessage("Password should contain at least 1 symbol");
    }
}