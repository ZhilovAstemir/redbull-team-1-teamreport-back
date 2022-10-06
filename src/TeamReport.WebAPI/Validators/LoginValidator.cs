using FluentValidation;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Validators;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(loginRequest =>
        loginRequest.Email)
            .NotEmpty().WithMessage("Email is required");
        RuleFor(loginRequest =>
        loginRequest.Password)
            .NotEmpty().Length(5, 30).WithMessage("Password is too short or empty");
    }
}