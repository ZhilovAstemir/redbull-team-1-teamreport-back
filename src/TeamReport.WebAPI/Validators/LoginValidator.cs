using FluentValidation;
using TeamReport.WebAPI.Models.Requests;

namespace TeamReport.WebAPI.Validators;

public class LoginValidator: AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(loginRequest =>
        loginRequest.Email)
            .NotEmpty().WithMessage("Email is requared");
        RuleFor(loginRequest =>
        loginRequest.Password)
            .NotEmpty().Length(8, 30).WithMessage("Password is requared");
    }
}
