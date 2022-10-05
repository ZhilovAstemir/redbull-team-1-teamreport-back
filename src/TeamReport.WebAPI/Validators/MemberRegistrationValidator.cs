﻿using FluentValidation;
using TeamReport.Domain.Models.Requests;

namespace TeamReport.Domain.Validators;

public class MemberRegistrationValidator:AbstractValidator<MemberRegistrationRequest>
{
    public MemberRegistrationValidator()
    {
        RuleFor(request =>
                request.Email)
            .NotEmpty().WithMessage("Email is required");
        RuleFor(request =>
                request.Password)
            .NotEmpty().Length(5, 30).WithMessage("Password is too short or empty");
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