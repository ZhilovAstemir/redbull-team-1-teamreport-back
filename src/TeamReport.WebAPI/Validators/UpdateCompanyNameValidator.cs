using FluentValidation;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Validators;

public class UpdateCompanyNameValidator:AbstractValidator<UpdateCompanyNameRequest>
{
    public UpdateCompanyNameValidator()
    {
        RuleFor(x => x.NewCompanyName).NotEmpty().Length(4, 100).WithMessage("New company name is too short or empty");
    }
}