using FluentValidation;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Validators;

public class ReportRequestValidator: AbstractValidator<ReportRequest>
{
    public ReportRequestValidator()
    {
        RuleFor(r =>
                r.Morale)
            .NotEmpty().WithMessage("Moral is requared")
            .IsInEnum().WithMessage("Invalid morale value");
        RuleFor(r =>
               r.MoraleComment)
           .NotEmpty().WithMessage("MoralComment is requared")
           .Length(1, 600).WithMessage("Invalid MoralComment length");
        RuleFor(r =>
                r.Stress)
            .NotEmpty().WithMessage("Stress is requared")
            .IsInEnum().WithMessage("Invalid stress value");
        RuleFor(r =>
               r.StressComment)
           .NotEmpty().WithMessage("StressComment is requared")
           .Length(1, 600).WithMessage("Invalid StressComment length");
        RuleFor(r =>
                r.Workload)
            .NotEmpty().WithMessage("Workload is requared")
            .IsInEnum().WithMessage("Invalid workload value");
        RuleFor(r =>
               r.WorkloadComment)
           .NotEmpty().WithMessage("WorkloadComment is requared")
           .Length(1, 600).WithMessage("Invalid WorkloadComment length");
        RuleFor(r =>
               r.High)
           .NotEmpty().WithMessage("High is requared")
           .Length(1, 600).WithMessage("Invalid High length");
        RuleFor(r =>
               r.Low)
           .NotEmpty().WithMessage("Low is requared")
           .Length(1, 600).WithMessage("Invalid Low length");
        RuleFor(r =>
               r.Else)
           .NotEmpty().WithMessage("Else is requared")
           .Length(1, 600).WithMessage("Invalid Else length");
        RuleFor(r =>
              r.StartDate)
          .NotEmpty().WithMessage("StartDate is requared");
        RuleFor(r =>
             r.EndDate)
         .NotEmpty().WithMessage("EndDate is requared");
    }
}
